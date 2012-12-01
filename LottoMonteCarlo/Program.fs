namespace RandomBits

module LottoMonteCarlo =

    open System
    open System.Collections.Generic

    [<EntryPoint>]
    let main argv = 
        Console.WriteLine ("")

        if argv.Length  = 0 then Console.WriteLine ("Enter the number of games to simulate between 0 and 2147483647")
        else

            let x = new RandomBits()

            let scoreBoard = Dictionary()
            scoreBoard.Add ("winQuickPick", 0)
            scoreBoard.Add ("loseQuickPick", 0)
            scoreBoard.Add ("winMyNumbers", 0)
            scoreBoard.Add ("loseMyNumbers", 0)

            let myNum1 = SByte.Parse argv.[1]
            let myNum2 = SByte.Parse argv.[2]

            let rec loop dec =
                match dec with
                | 0 -> ()
                | _ -> 
                    let quickPick = List.ofSeq (x.RndByteUniqueSeq (1y, 101y, 2))
                    let gameList = List.ofSeq (x.RndByteUniqueSeq (1y, 101y, 2))

                    let game = Map.ofList (List.zip gameList gameList)

                    if (game.ContainsKey (Seq.nth 0 quickPick)) && (game.ContainsKey (Seq.nth 1 quickPick)) then    
                        let count = scoreBoard.["winQuickPick"]
                        scoreBoard.["winQuickPick"] <- count + 1          
                    else
                        let count = scoreBoard.["loseQuickPick"]
                        scoreBoard.["loseQuickPick"] <- count + 1  

                    if (game.ContainsKey myNum1) && (game.ContainsKey myNum2) then    
                        let count = scoreBoard.["winMyNumbers"]
                        scoreBoard.["winMyNumbers"] <- count + 1          
                    else
                        let count = scoreBoard.["loseMyNumbers"]
                        scoreBoard.["loseMyNumbers"] <- count + 1  

                    loop (dec - 1)

            loop (Int32.Parse argv.[0])

            printfn "winQuickPick %i" scoreBoard.["winQuickPick"]
            printfn "loseQuickPick %i" scoreBoard.["loseQuickPick"]

            printfn ""

            printfn "winMyNumbers %i" scoreBoard.["winMyNumbers"]
            printfn "loseMyNumbers %i" scoreBoard.["loseMyNumbers"]
         
        Console.WriteLine ("")
                                          
        //printfn "Hit any key to exit."
        //System.Console.ReadKey() |> ignore
        0
