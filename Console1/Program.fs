namespace RandomBits

module console1 =
    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv

//        let x = new RandomBits("aF00000000000000")
        
        let x = new RandomBits()



//        let rec loop (acc : Map<sbyte, int>) dec =
//            match dec with
//            | 0 -> acc
//            | _ -> 
//                let n = x.RndSByte (0y, 10y)
//                if (acc.ContainsKey n) then 
//                    let count = acc.Item n
//                    let m = acc.Remove n
//                    loop (m.Add (n, (count + 1))) (dec - 1)
//                else loop (acc.Add (n, 1)) (dec - 1)
//            
//        Map.iter (fun k t -> printfn "%A %i" k t ) (loop Map.empty 100000)

        
//        let seq2 = x.RndBoolSeq 3
//        printfn "%A %s" seq2 "rndBoolSeq"
//        printfn "%A" (Seq.length seq2)
//
//        let seq1 = x.RndBoolSeq 84
//        printfn "%A %s" seq1 "rndBoolSeq"
//        printfn "%A %s" (Seq.length seq1) "rndBoolSeq"     
//        
//        let seq3 = x.RndBoolSeq 63
//        printfn "%A %s" seq3 "rndBoolSeq"
//        printfn "%A" (Seq.length seq3)
//        let seq4 = x.RndBoolSeq 64
//       // printfn "%i" (Seq.length seq4)
//        printfn "%A %s" seq4 "rndBoolSeq"
//        printfn "%i" (Seq.length seq4)
//        printfn "%A" (x.Consume64Count)
//        printfn ""
//        
//        let seq5 = x.RndBoolSeq 5
//        //printfn "%i" (Seq.length seq5)
//        printfn "%A %s" seq5 "rndBoolSeq"
//        printfn "%i" (Seq.length seq5)

//        printfn "%A %s" (x.RndBool()) "rndBool"
//        printfn "%A" (x.RndBool())
//        printfn "%A" (x.RndBool())
//        printfn "%A" (x.RndBool())
//        printfn "%A" (x.RndBool())
//        printfn "%A" (x.RndBool())
//        printfn ""

        printfn "%A %s " (x.RndSByte (System.SByte.MinValue, System.SByte.MaxValue)) "RndSByte  4"
        printfn "%A " (x.RndSByte (1y, 5y))
        printfn "%A " (x.RndSByte (-1y, 3y))
        printfn ""
        printfn "%A %s " (x.RndInt16 (System.Int16.MinValue, System.Int16.MaxValue)) "RndInt16"
        printfn "%A " (x.RndInt16 (1s, 5s))
        printfn "%A " (x.RndInt16 (-1s, 3s))
        printfn ""
        printfn "%A %s " (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue)) "RndInt32"
        printfn "%A " (x.RndInt32 (1, 5))
        printfn "%A " (x.RndInt32 (-1, 3))
        printfn ""
        printfn "%A " (x.RndInt64 (System.Int64.MinValue, System.Int64.MaxValue))
        printfn "%A " (x.RndInt64 (1L, 5L))
        printfn "%A " (x.RndInt64 (-1L, 3L))
        printfn ""
        printfn ""
        printfn "%A %s " (x.RndSByteSeq (System.SByte.MinValue, System.SByte.MaxValue, 4)) "RndSByte  4"
        printfn "%A " (x.RndSByteSeq (1y, 5y, 4))
        printfn "%A " (x.RndSByteSeq (-1y, 3y, 4))
        printfn ""
        printfn "%A %s " (x.RndInt16Seq (System.Int16.MinValue, System.Int16.MaxValue, 4)) "RndInt16  4"
        printfn "%A " (x.RndInt16Seq (1s, 5s, 4))
        printfn "%A " (x.RndInt16Seq (-1s, 3s, 4))
        printfn ""
        printfn "%A %s " (x.RndInt32Seq (System.Int32.MinValue, System.Int32.MaxValue, 4)) "RndInt32  4"
        printfn "%A " (x.RndInt32Seq (1, 5, 4))
        printfn "%A " (x.RndInt32Seq (-1, 3, 4))
        printfn ""
        printfn "%A %s " (x.RndInt64Seq (System.Int64.MinValue, System.Int64.MaxValue, 4))  "RndInt64  4"
        printfn "%A " (x.RndInt64Seq (1L, 5L, 4))
        printfn "%A " (x.RndInt64Seq (-1L, 3L, 4))
        printfn ""
        printfn ""
        printfn "%A %s " (x.RndSByteUniqueSeq (System.SByte.MinValue, System.SByte.MaxValue, 4)) "RndByte  4"
        printfn "%A " (x.RndSByteUniqueSeq (1y, 5y, 4))
        printfn "%A " (x.RndSByteUniqueSeq (-1y, 3y, 4))
        printfn ""
        printfn "%A %s " (x.RndInt16UniqueSeq (System.Int16.MinValue, System.Int16.MaxValue, 4)) "RndUint16  4"
        printfn "%A " (x.RndInt16UniqueSeq (1s, 5s, 4))
        printfn "%A " (x.RndInt16UniqueSeq (-1s, 3s, 4))
        printfn ""
        printfn "%A %s " (x.RndInt32UniqueSeq (System.Int32.MinValue, System.Int32.MaxValue, 4)) "RndInt32  4"
        printfn "%A " (x.RndInt32UniqueSeq (1, 5, 4))
        printfn "%A " (x.RndInt32UniqueSeq (-1, 3, 4))
       
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn "%A" (x.RndSByte (-128y, 127y))
//        printfn ""  
//        printfn "%A %s" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue)) "RndUint32  "
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn "%A" (x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue))
//        printfn ""
//
//        printfn "%A %s" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue) ) "RndInt32  "
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))
//        printfn "%A" (x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue))

//        let seq2 = x.RndInt64Seq 4
//        printfn "%A %s" seq2 "RndInt64Seq"
//        printfn ""
//        let seq2 = x.RndUint64Seq 4
//        printfn "%A %s" seq2 "RndUint64Seq"
//        printfn ""
//
//        let seq3 = x.RndByteSeq 63
//        printfn "%A %s" seq3 "RndByteSeq"
//        printfn "%A" (Seq.length seq3)
//        let seq4 = x.RndByteSeq 64
//        printfn "%A %s" seq4 "RndByteSeq"
//        printfn "%A" (Seq.length seq4)
//        let seq5 = x.RndByteSeq 65
//        printfn "%A %s" seq5 "RndByteSeq"
//        printfn "%A" (Seq.length seq5)
//
//        let seq1 = x.RndByteSeq 84
//        printfn "%A %s" seq1 "RndByteSeq"
//        printfn "%A" (Seq.length seq1) 
//
//
//        let seq2 = x.RndSByteSeq 3
//        printfn "%A %s" seq2 "RndSByteSeq"
//        printfn "%A" (Seq.length seq2)
//
//        let seq3 = x.RndSByteSeq 63
//        printfn "%A %s" seq3 "RndSByteSeq"
//        printfn "%A" (Seq.length seq3)
//        let seq4 = x.RndSByteSeq 64
//        printfn "%A %s" seq4 "RndSByteSeq"
//        printfn "%A" (Seq.length seq4)
//        let seq5 = x.RndSByteSeq 65
//        printfn "%A %s" seq5 "RndSByteSeq"
//        printfn "%A" (Seq.length seq5)
//
//        let seq1 = x.RndSByteSeq 84
//        printfn "%A %s" seq1 "RndSByteSeq"
//        printfn "%A" (Seq.length seq1)

//        printfn "%A %s" (x.RndByte()) "RndByte"
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn ""
//        printfn "%A %s" (x.RndSByte()) "RndSByte"
//        printfn "%A" (x.RndSByte())
//        printfn "%A" (x.RndSByte())
//        printfn "%A" (x.RndSByte())
//        printfn "%A" (x.RndSByte())
//        printfn "%A" (x.RndSByte())
//        printfn ""

        


//        printfn "%A %s" (x.RndUint32()) "rndUint32"
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn ""
//        printfn "%A %s" (x.RndInt32()) "rndInt32"
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn ""
//        printfn "%A %s" (x.RndUint32()) "rndUint32"
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn "%A" (x.RndUint32())
//        printfn ""
//        printfn "%A %s" (x.RndInt32()) "rndInt32"
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn "%A" (x.RndInt32())
//        printfn ""
//        printfn "%A %s" (x.RndUint64()) "rndUint64"
//        printfn "%A" (x.RndUint64())
//        printfn "%A" (x.RndUint64())
//        printfn "%A" (x.RndUint64())
//        printfn "%A" (x.RndUint64())
//        printfn "%A" (x.RndUint64())
//        printfn ""
//        printfn "%A %s" (x.RndInt64())  "rndInt64"
//        printfn "%A" (x.RndInt64())
//        printfn "%A" (x.RndInt64())
//        printfn "%A" (x.RndInt64())
//        printfn "%A" (x.RndInt64())
//        printfn "%A" (x.RndInt64())
        

//        let z = (Seq.nth 0 xx)
//        let zz = z.ToString()
//        printfn "%A" (Seq.nth 0 xx)
//
//        printfn "%i" (Seq.length xx)

        printfn "Hit any key to exit."
        System.Console.ReadKey() |> ignore
        0
