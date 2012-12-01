module RandomBitsTest.Int16

open NUnit.Framework
open RandomBits
open FsUnit

(* int16 and uint16 *)


[<Test>]
let ``random u and int16 from first mock byte`` () =
    let x = new RandomBits("7000000000000000")
    x.RndUint16() |> should equal 28672us
    //x.RndInt() |> should equal 28672s
    x.Consume64Count |> should equal 1L

[<Test>]
let ``random uint16 in range from first mock byte`` () =
    let x = new RandomBits("A800000000000000")
    x.RndUint16 (0us, 6us) |> should equal 5us

[<Test>]
let ``u Seq range length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16Seq (1us, 2us, 0))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Seq range of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndUint16Seq (1us, 1us, 2))) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``u Seq range of 1 returns predicted value`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16Seq (1us, 2us, 2)) |> should equal [1us; 1us]

[<Test>]
let ``u Seq range returns predicted values`` () =
    let x = new RandomBits("a800000000000000")
    List.ofSeq (x.RndUint16Seq (0us, 6us, 2)) |> should equal [5us; 2us]
    x.Consume64Count |> should equal 1L