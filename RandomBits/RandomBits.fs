namespace RandomBits

open System
open System.Collections.Concurrent
open System.Net
open System.Xml.Linq
open FSharpx
open FSharpx.DataStructures
open FSharpx.TypeProviders.Documents
open Microsoft.FSharp.Core

type private AnuJSON = StructuredJSON<Schema="""{"type":"string","length":100,"size":100,"data":["9b","b2"],"success":true}""">

type RandomBits =
    val private anuBlockCount : int  //number of 1024 byte blocks per request to ANU
    val private anuUrl : string
    val mutable private consume64Count : int64
    val private maxCache : int
    val private maxPersistCache : int
    val private persistPath : string
    val private reusePeristCache : bool
    val private theMock : string
    val private theQueue : ConcurrentQueue<uint64>

    val mutable private bitSource : uint64
    val mutable private ptr : uint64
 
    static member private retrieveBits (theQueue : ConcurrentQueue<uint64>) (anuUrl : string) (anuBlockCount : int) (mock : string) =
        use webClient = new WebClient()
        try
            let x = 
                if mock.Length > 0 then ""
                else webClient.DownloadString (anuUrl + "?type=hex16&length=" + anuBlockCount.ToString() + "&size=1024")
            if (mock.Length > 0) || AnuJSON(documentContent=x).Root.Success then 
                
                let f = (fun n ->
                    let nn = n.ToString()
                    let nnn = nn.Substring(1, (nn.Length - 2))  //lose quote marks around string
                    let l = Array.fold (fun l t -> match t with
                                                    | '0' -> 0u::l
                                                    | '1' -> 1u::l
                                                    | '2' -> 2u::l
                                                    | '3' -> 3u::l
                                                    | '4' -> 4u::l
                                                    | '5' -> 5u::l
                                                    | '6' -> 6u::l
                                                    | '7' -> 7u::l
                                                    | '8' -> 8u::l
                                                    | '9' -> 9u::l
                                                    | 'a' | 'A' -> 10u::l
                                                    | 'b' | 'B' -> 11u::l
                                                    | 'c' | 'C' -> 12u::l
                                                    | 'd' | 'D' -> 13u::l
                                                    | 'e' | 'E' -> 14u::l
                                                    | 'f' | 'F' -> 15u::l
                                                    | c -> failwith (c.ToString() + " not a hex character in cache feed.") ) [] (nnn.ToCharArray())
                                                    //seems to somehow not throw out of the two anon funs
                    let rec loop acc = function
                        | h1 :: h2 :: tl -> loop ((h1 |||  (h2  <<< 4)) :: acc) tl
                        | _ -> acc
                
                    let lBytes = loop [] l  //not really bytes, since they are int32
                 
                    let rec loop2 = function
                        | h0 :: h1 :: h2 :: h3 :: h4 :: h5 :: h6 :: h7 :: tl -> 
                            theQueue.Enqueue ( ((uint64 h0) <<< 56) ||| ((uint64 h1) <<< 48)  ||| ((uint64 h2) <<< 40)  ||| ((uint64 h3) <<< 32)  
                                                ||| ((uint64 h4) <<< 24) ||| ((uint64 h5) <<< 16)  ||| ((uint64 h6) <<< 8)  ||| (uint64 h7) )
                            loop2 tl
                        | _ -> ()
                    loop2 lBytes)

                if mock.Length > 0 then Seq.iter f (seq {yield mock})
                else Seq.iter f (AnuJSON(documentContent=x).Root.GetDatas())
                
                ()
            else ()
        with
            | _ -> ()
        
    static member private byteOfBit n x o =
        let x' = (1UL <<< ((64 - o) - n))
        if (x' &&& x) = x' then (1uy <<< (8 - n))
        else 0uy

    new () as this =
        {anuBlockCount = 20;
        anuUrl = "https://qrng.anu.edu.au/API/jsonI.php";
        consume64Count = 0L;
        maxCache = 4096;
        maxPersistCache = 0;
        persistPath = "";
        reusePeristCache = false;
        theMock = "";
        theQueue = ConcurrentQueue<uint64>();
        bitSource = 0UL;
        ptr = 0UL;
         }

        then
        RandomBits.retrieveBits this.theQueue this.anuUrl this.anuBlockCount ""

    new (mock) as this =
        {anuBlockCount = 0;
        anuUrl = "";
        maxCache = 1;
        maxPersistCache = 0;
        consume64Count = 0L;
        persistPath = "";
        reusePeristCache = false;
        theMock = "\"" + mock + "\"";
        theQueue = ConcurrentQueue<uint64>();
        bitSource = 0UL;
        ptr = 0UL;
         }

        then
        if this.theMock.Length < 18 then failwith "Mock string must represent at least one 64-bit integer"

        let l = Array.fold (fun l t -> match t with
                                        | '0' -> 0u::l
                                        | '1' -> 1u::l
                                        | '2' -> 2u::l
                                        | '3' -> 3u::l
                                        | '4' -> 4u::l
                                        | '5' -> 5u::l
                                        | '6' -> 6u::l
                                        | '7' -> 7u::l
                                        | '8' -> 8u::l
                                        | '9' -> 9u::l
                                        | 'a' | 'A' -> 10u::l
                                        | 'b' | 'B' -> 11u::l
                                        | 'c' | 'C' -> 12u::l
                                        | 'd' | 'D' -> 13u::l
                                        | 'e' | 'E' -> 14u::l
                                        | 'f' | 'F' -> 15u::l
                                        | c -> failwith (c.ToString() + " not a hex character in mock.") ) [] (mock.ToCharArray())
                                        //seems to somehow not throw out of the two anon funs interfaceretrieveBits, so repeating it here

        RandomBits.retrieveBits this.theQueue "" 0 this.theMock

    member private this.next64() =
        let (success, ru64) = this.theQueue.TryDequeue()
        if success then 
            this.consume64Count <- this.consume64Count + 1L
            ru64
        else 
            RandomBits.retrieveBits this.theQueue this.anuUrl this.anuBlockCount  this.theMock
            let (success, ru64) = this.theQueue.TryDequeue()
            this.consume64Count <- this.consume64Count + 1L
            ru64                    

    member private this.nextBit() =
        
        this.ptr <- this.ptr >>> 1
        if this.ptr = 0UL then
            this.bitSource <- this.next64()
            this.ptr <- 9223372036854775808UL

        ((this.ptr &&& this.bitSource) = this.ptr)

    member this.ANU_BlockCount = this.anuBlockCount
    member this.ANU_Url = this.anuUrl
    member this.CacheLength = this.theQueue.Count
    member this.Consume64Count = this.consume64Count
    member this.MaxCache = this.maxCache
    member this.MaxPersistCache = this.maxPersistCache
    member this.PersistCacheLength = 0
    member this.PersistPath = this.persistPath
    member this.ReusePeristCache = this.reusePeristCache

    member this.RndBool() =
        let x = this.next64()

        let x' = (1UL <<< 63)
        if (x' &&& x) = x' then true
        else false

    member this.RndBoolSeq length =

        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        seq {for i = 1 to length do
                yield this.nextBit()
            }

    member this.RndUint16() = 
        let x = this.next64()

        let int16OfBit n =
            let x' = (1UL <<< (64 - n))
            if (x' &&& x) = x' then (1us <<< (16 - n))
            else 0us

        (int16OfBit 1) ||| (int16OfBit 2) ||| (int16OfBit 3) ||| (int16OfBit 4) ||| (int16OfBit 5) ||| (int16OfBit 6) 
            ||| (int16OfBit 7) ||| (int16OfBit 8) ||| (int16OfBit 9) ||| (int16OfBit 10) ||| (int16OfBit 11)
            ||| (int16OfBit 12) ||| (int16OfBit 13) ||| (int16OfBit 14) ||| (int16OfBit 15) ||| (int16OfBit 16)

    member this.RndUint16 (inclLower, exlUpper) =
        let range =  exlUpper -  inclLower 
        if range < 1us then failwith "range must be greater than 1"

        let upper = exlUpper - (inclLower + 1us)

        //bit shifts to determine highest order bit of upper (power)
        let pwr =       
            let rec loop u acc =
                match (u >>> 1) with
                | 0us -> acc
                | u' -> loop u' (acc + 1)
            loop upper 0

        //random walk down to array of smallest power of 2 including upper
        let rec loop (acc : uint16) pwrDec =
            match pwrDec with
            | -1 -> acc
            | _ ->
                if this.nextBit() then loop (acc +  (1us <<< pwrDec)) (pwrDec - 1)
                else loop acc (pwrDec - 1)

        //greater than upper? try again
        let rec loop2 = function
            | x when x > upper -> loop2 (loop 0us pwr)
            | x -> x

        let myRnd = loop2 (loop 0us pwr)

        myRnd + inclLower

    member this.RndUint16Seq (inclLower, exlUpper, length)  = 
        let range =  exlUpper -  inclLower 
        if range < 1us then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        let k = ref length

        seq {for i = 1 to length do
                yield this.RndUint16 (inclLower, exlUpper)
            }

    member this.RndInt16() = int16 (this.RndUint16())
    
    member this.RndUint32() = 
        let x = this.next64()

        let int32OfBit n =
            let x' = (1UL <<< (64 - n))
            if (x' &&& x) = x' then (1u <<< (32 - n))
            else 0u

        (int32OfBit 1) ||| (int32OfBit 2) ||| (int32OfBit 3) ||| (int32OfBit 4) ||| (int32OfBit 5) ||| (int32OfBit 6) 
            ||| (int32OfBit 7) ||| (int32OfBit 8) ||| (int32OfBit 9) ||| (int32OfBit 10) ||| (int32OfBit 11)
            ||| (int32OfBit 12) ||| (int32OfBit 13) ||| (int32OfBit 14) ||| (int32OfBit 15) ||| (int32OfBit 16)
            ||| (int32OfBit 17) ||| (int32OfBit 18) ||| (int32OfBit 19) ||| (int32OfBit 20) ||| (int32OfBit 21)
            ||| (int32OfBit 22) ||| (int32OfBit 23) ||| (int32OfBit 24) ||| (int32OfBit 25) ||| (int32OfBit 26)
            ||| (int32OfBit 27) ||| (int32OfBit 28) ||| (int32OfBit 29) ||| (int32OfBit 30) ||| (int32OfBit 31)

    member this.RndInt32() = int32 (this.RndUint32())

    member this.RndUint64() = this.next64()

    member this.RndInt64() = int64 (this.next64())
    
    (* byte and ubyte *)
    member this.RndUbyte() = 
        let x = this.next64()
        
        (RandomBits.byteOfBit 1 x 0) ||| (RandomBits.byteOfBit 2 x 0) ||| (RandomBits.byteOfBit 3 x 0) ||| (RandomBits.byteOfBit 4 x 0) 
            ||| (RandomBits.byteOfBit 5 x 0) ||| (RandomBits.byteOfBit 6 x 0) ||| (RandomBits.byteOfBit 7 x 0) ||| (RandomBits.byteOfBit 8 x 0)

    member this.RndUbyte (inclLower, exlUpper) =
        let range =  exlUpper -  inclLower 
        if range < 1uy then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper) 
        
        let upper = exlUpper - (inclLower + 1uy)

        //bit shifts to determine highest order bit of upper (power)
        let pwr =       
            let rec loop u acc =
                match (u >>> 1) with
                | 0uy -> acc
                | u' -> loop u' (acc + 1)
            loop upper 0

        //random walk down to array of smallest power of 2 including upper
        let rec loop (acc : byte) pwrDec =
            match pwrDec with
            | -1 -> acc
            | _ ->
                if this.nextBit() then loop (acc +  (1uy <<< pwrDec)) (pwrDec - 1)
                else loop acc (pwrDec - 1)

        //greater than upper? try again
        let rec loop2 = function
            | x when x > upper -> loop2 (loop 0uy pwr)
            | x -> x

        let myRnd = loop2 (loop 0uy pwr)

        myRnd + inclLower

    member this.RndByte() = sbyte (this.RndUbyte())

    member this.RndByte ((inclLower : sbyte), (exlUpper : sbyte)) = 
        let range =  exlUpper -  inclLower 
        if range < 1y then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        
        let x = 
            if inclLower < 0y then this.RndUint16 (0us, (uint16 ((int16 exlUpper) - (int16 inclLower))))
            else this.RndUint16 (0us, (uint16 (exlUpper - inclLower)))

        sbyte( (int16 x) + (int16 inclLower) )

    member this.RndUbyteSeq length = 

        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        let k = ref length

        seq {while !k > 0 do
                let x = this.next64()

                let n' =
                    if !k > 8 then 8
                    else !k
                 
                k := !k - 8

                for i = 1 to n' do
                    let k = (i - 1) * 8
                    
                    yield ((RandomBits.byteOfBit 1 x k) ||| (RandomBits.byteOfBit 2 x k) ||| (RandomBits.byteOfBit 3 x k) ||| (RandomBits.byteOfBit 4 x k) 
                        ||| (RandomBits.byteOfBit 5 x k) ||| (RandomBits.byteOfBit 6 x k) ||| (RandomBits.byteOfBit 7 x k) ||| (RandomBits.byteOfBit 8 x k))
             }

    member this.RndUbyteSeq (inclLower, exlUpper, length)  = 
        let range =  exlUpper -  inclLower 
        if range < 1uy then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        seq {for i = 1 to length do
                yield this.RndUbyte (inclLower, exlUpper)
            }

    member this.RndUbyteUniqueSeq (inclLower, exlUpper, length)  = 

        let range =  exlUpper -  inclLower 
        if range < 1uy then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        if length > (int range) then invalidArg "length" (sprintf "seq length %i cannot be greater than range %i - %i of unique values" length inclLower exlUpper)
        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        let count = ref 0uy
        let h = ref (PairingHeap.empty false)
        
        seq {           
            for i = 1 to length do
                let x = this.RndUbyte (inclLower, (exlUpper - !count))
                let x' = Seq.fold (fun x t -> 
                                    if x >= t then x + 1uy
                                    else x) x (!h)
                yield x'
                count := !count + 1uy
                h := (!h).Insert x'
            }

    member this.RndByteSeq length = 

        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        seq {let k = ref length
             while !k > 0 do
                let x = this.next64()

                let n' =
                    if !k > 8 then 8
                    else !k
                 
                for i = 1 to n' do
                    let k = (i - 1) * 8
                    
                    yield sbyte ((RandomBits.byteOfBit 1 x k) ||| (RandomBits.byteOfBit 2 x k) ||| (RandomBits.byteOfBit 3 x k) ||| (RandomBits.byteOfBit 4 x k) 
                        ||| (RandomBits.byteOfBit 5 x k) ||| (RandomBits.byteOfBit 6 x k) ||| (RandomBits.byteOfBit 7 x k) ||| (RandomBits.byteOfBit 8 x k))
                    
                k := !k - 8
             }

    member this.RndByteSeq (inclLower, exlUpper, length)  = 
        let range =  exlUpper -  inclLower 
        if range < 1y then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        let k = ref length

        seq {for i = 1 to length do
                yield this.RndByte (inclLower, exlUpper)
            }

    member this.RndByteUniqueSeq (inclLower, exlUpper, length)  = 
        let range =  exlUpper -  inclLower 
        if range < 1y then invalidArg  "range" (sprintf "%i %i range must be greater than 0" inclLower exlUpper)
        if length > (int range) then invalidArg "length" (sprintf "seq length %i cannot be greater than range %i - %i of unique values" length inclLower exlUpper)
        if length < 1 then invalidArg  "length" (sprintf "%i must be greater than 0" length)

        let count = ref 0y
        let h = ref (PairingHeap.empty false)
        
        seq {           
            for i = 1 to length do
                let x = this.RndByte (inclLower, (exlUpper - !count))
                let x' = Seq.fold (fun x t -> 
                                    if x >= t then x + 1y
                                    else x) x (!h)
                yield x'
                count := !count + 1y
                h := (!h).Insert x'
            }
        

    with
    interface IDisposable with
        member this.Dispose() = ()

