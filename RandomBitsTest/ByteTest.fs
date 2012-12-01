module RandomBitsTest.Byte

open NUnit.Framework
open RandomBits
open FsUnit

(* byte and ubyte *)

[<Test>]
let ``random sbyte in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndByte (-12y, -2y) |> should equal -4y

[<Test>]
let ``random u and byte from first mock byte`` () =
    let x = new RandomBits("7800000000000000")
    x.RndUbyte() |> should equal 120uy
    x.RndByte() |> should equal 120y
    x.Consume64Count |> should equal 2L

[<Test>]
let ``random ubyte in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUbyte (0uy, 6uy) |> should equal 5uy

[<Test>]
let ``s Seq 8 consumes 1 64-bit cache block`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndByteSeq 8) |> should equal [120y; 22y; 0y; 17y; 34y; 16y; 1y; 51y]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``s Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndByteSeq 0)) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndByteSeq (1y, 2y, 0))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndByteSeq (1y, 2y, 2)) |> should equal [1y; 1y]

[<Test>]
let ``s Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndByteSeq (1y, 1y, 2))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndByteSeq (0y, 6y, 2)) |> should equal [5y; 2y]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``s Seq returns predicted values`` () =
    let x = new RandomBits("7816000000000000")
    List.ofSeq (x.RndUbyteSeq 2) |> should equal [120uy; 22uy]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``s Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndByteUniqueSeq (2y, 12y, 11) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndByteUniqueSeq (1y, 2y, 0))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndByteUniqueSeq (1y, 1y, 2))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``s Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndByteUniqueSeq (1y, 2y, 1)) |> should equal [1us]

[<Test>]
let ``s Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndByteUniqueSeq (0y, 6y, 2)) |> should equal [5y; 2y]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Seq 9 consumes 2 64-bit cache blocks`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndUbyteSeq 9) |> should equal [120uy; 22uy; 0uy; 17uy; 34uy; 16uy; 1uy; 51uy; 120uy]
    x.Consume64Count |> should equal 2L

[<Test>]
let ``u Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUbyteSeq 0)) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUbyteSeq (1uy, 2uy, 0))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUbyteSeq (1uy, 2uy, 2)) |> should equal [1us; 1us]

[<Test>]
let ``u Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUbyteSeq (1uy, 1uy, 2))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUbyteSeq (0uy, 6uy, 2)) |> should equal [5uy; 2uy]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Seq returns predicted values`` () =
    let x = new RandomBits("7816000000000000")
    List.ofSeq (x.RndUbyteSeq 2) |> should equal [120uy; 22uy]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndUbyteUniqueSeq (2uy, 12uy, 11) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUbyteUniqueSeq (1uy, 2uy, 1)) |> should equal [1us]

[<Test>]
let ``u Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUbyteUniqueSeq (1uy, 1uy, 2))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUbyteUniqueSeq (1uy, 2uy, 0))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUbyteUniqueSeq (0uy, 6uy, 2)) |> should equal [5uy; 2uy]
    x.Consume64Count |> should equal 1L