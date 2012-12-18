module RandomBitsTest.Int16

open NUnit.Framework
open RandomBits
open FsUnit

[<Test>]
let ``random int16 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndInt16 (-12s, -2s) |> should equal -4s

[<Test>]
let ``random int16 in range from first mock byte 2`` () =
    let x = new RandomBits("A800000000000000")
    x.RndInt16 (-2s, 6s) |> should equal 3s

[<Test>]
let ``random u and int16 from first mock byte`` () =
    let x = new RandomBits("7000000000000000")
    x.RndUint16() |> should equal 28672us
    x.RndInt16() |> should equal 28672s
    x.Consume64Count |> should equal 2L

[<Test>]
let ``random uint16 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint16 (0us, 6us) |> should equal 5us

[<Test>]
let ``random u and int16 in range of max range`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint16 (System.UInt16.MinValue, System.UInt16.MaxValue) |> should equal 43008us
    x.RndInt16 (System.Int16.MinValue, System.Int16.MaxValue) |> should equal -32768s

[<Test>]
let ``s Seq length of 8 consumes 2 64-bit cache block`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndInt16Seq 8) |> should equal [30742s; 17s; 8720s; 307s; 30742s; 17s; 8720s; 307s]
    x.Consume64Count |> should equal 2L

[<Test>]
let ``s Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt16Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt16Seq (1s, 2s, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt16Seq (1s, 2s, 2)) |> should equal [1s; 1s]

[<Test>]
let ``s Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt16Seq (1s, 1s, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of max range`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq (x.RndInt16Seq (System.Int16.MinValue, System.Int16.MaxValue, 255))).Length |> should equal 255
    x.Consume64Count |> should equal 64L

[<Test>]
let ``s Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt16Seq (0s, 6s, 2)) |> should equal [5s; 2s]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``s Unique Seq of length = range succeed`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq ( x.RndInt16UniqueSeq (2s, 12s, 10))).Length |> should equal 10

[<Test>]
let ``s Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndInt16UniqueSeq (2s, 12s, 11) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt16UniqueSeq (1s, 2s, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt16UniqueSeq (1s, 1s, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt16UniqueSeq (1s, 2s, 1)) |> should equal [1s]

[<Test>]
let ``s Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt16UniqueSeq (0s, 6s, 2)) |> should equal [5s; 2s]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Seq length of 9 consumes 3 64-bit cache blocks`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndUint16Seq 9) |> should equal [30742us; 17us; 8720us; 307us; 30742us; 17us; 8720us; 307us; 30742us]
    x.Consume64Count |> should equal 3L

[<Test>]
let ``u Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16Seq (1us, 2us, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16Seq (1us, 1us, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16Seq (1us, 2us, 2)) |> should equal [1us; 1us]

[<Test>]
let ``u Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16Seq (0us, 6us, 2)) |> should equal [5us; 2us]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Unique Seq of length = range succeed`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq ( x.RndUint16UniqueSeq (2us, 12us, 10))).Length |> should equal 10

[<Test>]
let ``u Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndUint16UniqueSeq (2us, 12us, 11) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16UniqueSeq (1us, 2us, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16UniqueSeq (1us, 2us, 1)) |> should equal [1us]

[<Test>]
let ``u Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16UniqueSeq (1us, 1us, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16UniqueSeq (0us, 6us, 2)) |> should equal [5us; 2us]
    x.Consume64Count |> should equal 1L