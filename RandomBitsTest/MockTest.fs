module RandomBitsTest.Mock

open NUnit.Framework
open RandomBits
open FsUnit

[<Test>]
let ``all hex chars work in mock string`` () =
    let x = new RandomBits("1234567890abcdefABCDEF0000000000")
    x.CacheLength |> should equal 2

[<Test>]
let ``non 64-bit mock string lengths truncate`` () =
    let x = new RandomBits("1234567890abcdefABCDEF000000000")
    x.CacheLength |> should equal 1

    let x = new RandomBits("1234567890abcdefABCDEF00000000011")
    x.CacheLength |> should equal 2
       
[<Test>]
let ``non-hex mock string should fail`` () =
    (fun () -> (new RandomBits("800000000000000v")) |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``too short mock string should fail`` () =
    (fun () -> (new RandomBits("800000000000000")) |> ignore) |> should throw typeof<System.Exception>

