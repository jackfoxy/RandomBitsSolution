module RandomBitsTest.Bool

open NUnit.Framework
open RandomBits
open FsUnit

[<Test>]
let ``2 rndBool calls consume 2 64-bit cache blocks`` () =
    let x = new RandomBits("7fffffffffffffff")
    let a = x.RndBool() 
    let b = x.RndBool()
    x.Consume64Count |> should equal 2L

[<Test>]
let ``first bit false`` () =
    let x = new RandomBits("7fffffffffffffff")
    x.RndBool() |> should be False

[<Test>]
let ``first bit true`` () =
    let x = new RandomBits("8000000000000000")
    x.RndBool() |> should be True

[<Test>]
let ``Seq length of less than 1 fails`` () =
    let x = new RandomBits("a800000000000000")
    (fun () -> (List.ofSeq (x.RndBoolSeq 0)) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``Seq of 64 consumes 1 64-bit cache block`` () =
    let x = new RandomBits("aF00000000000000")
    let a = List.ofSeq (x.RndBoolSeq 64) 
    x.Consume64Count |> should equal 1L

[<Test>]
let ``Seq of 65 consumes 2 64-bit cache blocks`` () =
    let x = new RandomBits("aF00000000000000")
    let a = List.ofSeq (x.RndBoolSeq 65) 
    x.Consume64Count |> should equal 2L

[<Test>]
let ``Seq returns predicted values`` () =
    let x = new RandomBits("aF00000000000000")
    List.ofSeq (x.RndBoolSeq 5) |> should equal [true; false; true; false; true]