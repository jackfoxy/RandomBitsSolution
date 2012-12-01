namespace RandomBits

module console1 =
    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv

//        let x = new RandomBits("aF00000000000000")
        
        let x = new RandomBits()



        let rec loop (acc : Map<sbyte, int>) dec =
            match dec with
            | 0 -> acc
            | _ -> 
                let n = x.RndByte (0y, 10y)
                if (acc.ContainsKey n) then 
                    let count = acc.Item n
                    let m = acc.Remove n
                    loop (m.Add (n, (count + 1))) (dec - 1)
                else loop (acc.Add (n, 1)) (dec - 1)
            
        Map.iter (fun k t -> printfn "%A %i" k t ) (loop Map.empty 100000)

        
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


//        printfn "%A %s" (x.RndByte (-128y, 127y)) "RndByte  6"
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))
//        printfn "%A" (x.RndByte (-128y, 127y))

//        printfn "%A %s" (x.RndUbyteUniqueSeq (2uy, 12uy, 10) ) "RndByte  6"

//        printfn "%A %s" (x.RndByteSeq (-1y, -2y, 2) ) "RndByte  6"
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))
//        printfn "%A" (x.RndByteSeq (-12y, -2y, 2))

//        let seq2 = x.RndUbyteSeq 3
//        printfn "%A %s" seq2 "rndUbyteSeq"
//        printfn "%A" (Seq.length seq2)
//
//        let seq3 = x.RndUbyteSeq 63
//        printfn "%A %s" seq3 "rndUbyteSeq"
//        printfn "%A" (Seq.length seq3)
//        let seq4 = x.RndUbyteSeq 64
//        printfn "%A %s" seq4 "rndUbyteSeq"
//        printfn "%A" (Seq.length seq4)
//        let seq5 = x.RndUbyteSeq 65
//        printfn "%A %s" seq5 "rndUbyteSeq"
//        printfn "%A" (Seq.length seq5)
//
//        let seq1 = x.RndUbyteSeq 84
//        printfn "%A %s" seq1 "rndUbyteSeq"
//        printfn "%A" (Seq.length seq1) 
//
//
//        let seq2 = x.RndByteSeq 3
//        printfn "%A %s" seq2 "rndByteSeq"
//        printfn "%A" (Seq.length seq2)
//
//        let seq3 = x.RndByteSeq 63
//        printfn "%A %s" seq3 "rndByteSeq"
//        printfn "%A" (Seq.length seq3)
//        let seq4 = x.RndByteSeq 64
//        printfn "%A %s" seq4 "rndByteSeq"
//        printfn "%A" (Seq.length seq4)
//        let seq5 = x.RndByteSeq 65
//        printfn "%A %s" seq5 "rndByteSeq"
//        printfn "%A" (Seq.length seq5)
//
//        let seq1 = x.RndByteSeq 84
//        printfn "%A %s" seq1 "rndByteSeq"
//        printfn "%A" (Seq.length seq1)

//        printfn "%A %s" (x.RndUbyte()) "rndUbyte"
//        printfn "%A" (x.RndUbyte())
//        printfn "%A" (x.RndUbyte())
//        printfn "%A" (x.RndUbyte())
//        printfn "%A" (x.RndUbyte())
//        printfn "%A" (x.RndUbyte())
//        printfn ""
//        printfn "%A %s" (x.RndByte()) "rndByte"
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn "%A" (x.RndByte())
//        printfn ""

        


//        printfn "%A %s" (x.RndUint16()) "rndUint16"
//        printfn "%A" (x.RndUint16())
//        printfn "%A" (x.RndUint16())
//        printfn "%A" (x.RndUint16())
//        printfn "%A" (x.RndUint16())
//        printfn "%A" (x.RndUint16())
//        printfn ""
//        printfn "%A %s" (x.RndInt16()) "rndInt16"
//        printfn "%A" (x.RndInt16())
//        printfn "%A" (x.RndInt16())
//        printfn "%A" (x.RndInt16())
//        printfn "%A" (x.RndInt16())
//        printfn "%A" (x.RndInt16())
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
