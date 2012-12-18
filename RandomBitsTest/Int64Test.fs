module RandomBitsTest.Int64

open NUnit.Framework
open RandomBits
open FsUnit

[<Test>]
let ``random u and int64 from first mock byte`` () =
    let x = new RandomBits("7000000000000000")
    x.RndUint64() |> should equal 8070450532247928832UL
    x.RndInt64() |> should equal 8070450532247928832L
    x.Consume64Count |> should equal 2L

[<Test>]
let ``random uint64 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint64 (0UL, 6UL) |> should equal 5UL

[<Test>]
let ``random u and int64 in range of max range`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint64 (System.UInt64.MinValue, System.UInt64.MaxValue) |> should equal 12105675798371893248UL
    x.RndInt64 (System.Int64.MinValue, System.Int64.MaxValue) |> should equal 2882303761517117440L

[<Test>]
let ``s Seq length of 8 consumes 8 64-bit cache block`` () =
    let x = new RandomBits("7816001122100133")
    List.ofSeq (x.RndInt64Seq 8) |> should equal [8653103807624905011L; 8653103807624905011L; 8653103807624905011L; 8653103807624905011L; 8653103807624905011L; 8653103807624905011L; 8653103807624905011L; 8653103807624905011L]
    x.Consume64Count |> should equal 8L

[<Test>]
let ``s Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt64Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq length of 9 consumes 9 64-bit cache blocks`` () =
    let x = new RandomBits("7816001122100133")
    let y = List.ofSeq (x.RndUint64Seq 9) 
    y |> should equal [8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL; 8653103807624905011UL]
    x.Consume64Count |> should equal 9L

[<Test>]
let ``s Seq range length of less than 1 fails2`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt64Seq (1L, 2L, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt64Seq (1L, 2L, 2)) |> should equal [1L; 1L]

[<Test>]
let ``s Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndInt64Seq (1L, 1L, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``s Seq range of max range`` () =
    let x = new RandomBits("7816001122100133")
    (List.ofSeq (x.RndInt64Seq (System.Int64.MinValue, System.Int64.MaxValue, 255))).Length |> should equal 255
    x.Consume64Count |> should equal 255L

[<Test>]
let ``s Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndInt64Seq (0L, 6L, 2)) |> should equal [5L; 2L]
    x.Consume64Count |> should equal 1L

[<Test>]
let ``u Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint64Seq 0)) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint64Seq (1UL, 2UL, 0))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint64Seq (1UL, 1UL, 2))) |> ignore) |> should throw typeof<System.ArgumentException>

[<Test>]
let ``u Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint64Seq (1UL, 2UL, 2)) |> should equal [1UL; 1UL]

[<Test>]
let ``u Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint64Seq (0UL, 6UL, 2)) |> should equal [5UL; 2UL]
    x.Consume64Count |> should equal 1L