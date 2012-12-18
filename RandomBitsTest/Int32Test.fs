module RandomBitsTest.Int32

open NUnit.Framework
open RandomBits
open FsUnit

[<Test>]
let ``random int32 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndInt32 (-12, -2) |> should equal -4

[<Test>]
let ``random int32 in range from first mock byte 2`` () =
    let x = new RandomBits("A800000000000000")
    x.RndInt32 (-2, 6) |> should equal 3

[<Test>]
let ``random u and int32 from first mock byte`` () =
    let x = new RandomBits("7000000000000000")
    x.RndUint32() |> should equal 1879048192u
    x.RndInt32() |> should equal 1879048192
    x.Consume64Count |> should equal 2L

[<Test>]
let ``random uint32 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint32 (0u, 6u) |> should equal 5u

[<Test>]
let ``random u and int32 in range of max range`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint32 (System.UInt32.MinValue, System.UInt32.MaxValue) |> should equal 2818572288u
    x.RndInt32 (System.Int32.MinValue, System.Int32.MaxValue) |> should equal -2147483648

[<Test>]
let ``s Seq length of 8 consumes 4 64-bit cache block`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndInt32Seq 8) |> should equal [2014707729; 571474227; 2014707729; 571474227; 2014707729; 571474227; 2014707729; 571474227]
    x.Consume64Count |> should equal 4L

[<Test>]
let ``s Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt32Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range length of less than 1 fails2`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt32Seq (1, 2, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt32Seq (1, 2, 2)) |> should equal [1; 1]

[<Test>]
let ``s Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt32Seq (1, 1, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of max range`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq (x.RndInt32Seq (System.Int32.MinValue, System.Int32.MaxValue, 255))).Length |> should equal 255
    x.Consume64Count |> should equal 128L

[<Test>]
let ``s Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt32Seq (0, 6, 2)) |> should equal [5; 2]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``s Unique Seq of length = range succeed`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq ( x.RndInt32UniqueSeq (2, 12, 10))).Length |> should equal 10

[<Test>]
let ``s Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndInt32UniqueSeq (2, 12, 11) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt32UniqueSeq (1, 2, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt32UniqueSeq (1, 1, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt32UniqueSeq (1, 2, 1)) |> should equal [1]

[<Test>]
let ``s Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt32UniqueSeq (0, 6, 2)) |> should equal [5; 2]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Seq length of 9 consumes 5 64-bit cache blocks`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndUint32Seq 9) |> should equal [2014707729u; 571474227u; 2014707729u; 571474227u; 2014707729u; 571474227u; 2014707729u; 571474227u; 2014707729u]
    x.Consume64Count |> should equal 5L

[<Test>]
let ``u Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint32Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint32Seq (1u, 2u, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint32Seq (1u, 1u, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint32Seq (1u, 2u, 2)) |> should equal [1u; 1u]

[<Test>]
let ``u Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint32Seq (0u, 6u, 2)) |> should equal [5u; 2u]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Unique Seq of length = range succeed`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq ( x.RndUint32UniqueSeq (2u, 12u, 10))).Length |> should equal 10

[<Test>]
let ``u Unique Seq of length gt range should fail`` () =
    let x = new RandomBits("7816001122100133")
    (fun () -> x.RndUint32UniqueSeq (2u, 12u, 11) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint32UniqueSeq (1u, 2u, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint32UniqueSeq (1u, 2u, 1)) |> should equal [1u]

[<Test>]
let ``u Unique Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint32UniqueSeq (1u, 1u, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Unique Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint32UniqueSeq (0u, 6u, 2)) |> should equal [5u; 2u]
    x.Consume64Count |> should equal 1L