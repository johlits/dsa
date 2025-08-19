module Day5

open System
open System.IO

type Opcode =
    | Add
    | Mult
    | Input
    | Output
    | JumpIfTrue
    | JumpIfFalse
    | LessThan
    | Equals
    | Halt

type ParameterMode =
    | Position = 0
    | Immediate = 1

let inputValue = 5 // System ID for the ship's thermal radiator controller

let rec add (arr: int list) i mode1 mode2 mode3 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let param3 = arr.[i + 3]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if mode3 = ParameterMode.Position && param3 < List.length arr then
        let newArr = arr |> List.mapi (fun j x -> if j = param3 then value1 + value2 else x)
        process newArr (i + 4)
    else
        invalidOp "Invalid write index"

and mult (arr: int list) i mode1 mode2 mode3 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let param3 = arr.[i + 3]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if mode3 = ParameterMode.Position && param3 < List.length arr then
        let newArr = arr |> List.mapi (fun j x -> if j = param3 then value1 * value2 else x)
        process newArr (i + 4)
    else
        invalidOp "Invalid write index"

and input (arr: int list) i =
    let param1 = arr.[i + 1]
    if param1 < List.length arr then
        let newArr = arr |> List.mapi (fun j x -> if j = param1 then inputValue else x)
        process newArr (i + 2)
    else
        invalidOp "Invalid write index"

and output (arr: int list) i mode1 =
    let param1 = arr.[i + 1]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    Console.WriteLine(value1)
    process arr (i + 2)

and jumpIfTrue (arr: int list) i mode1 mode2 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if value1 <> 0 then
        process arr value2
    else
        process arr (i + 3)

and jumpIfFalse (arr: int list) i mode1 mode2 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if value1 = 0 then
        process arr value2
    else
        process arr (i + 3)

and lessThan (arr: int list) i mode1 mode2 mode3 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let param3 = arr.[i + 3]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if mode3 = ParameterMode.Position && param3 < List.length arr then
        let newValue = if value1 < value2 then 1 else 0
        let newArr = arr |> List.mapi (fun j x -> if j = param3 then newValue else x)
        process newArr (i + 4)
    else
        invalidOp "Invalid write index"

and equals (arr: int list) i mode1 mode2 mode3 =
    let param1 = arr.[i + 1]
    let param2 = arr.[i + 2]
    let param3 = arr.[i + 3]
    let value1 = if mode1 = ParameterMode.Position && param1 < List.length arr then arr.[param1] else param1
    let value2 = if mode2 = ParameterMode.Position && param2 < List.length arr then arr.[param2] else param2
    if mode3 = ParameterMode.Position && param3 < List.length arr then
        let newValue = if value1 = value2 then 1 else 0
        let newArr = arr |> List.mapi (fun j x -> if j = param3 then newValue else x)
        process newArr (i + 4)
    else
        invalidOp "Invalid write index"

and process (arr: int list) i =
    let opcodeValue = arr.[i] % 100
    let mode1Value = arr.[i] / 100 % 10
    let mode2Value = arr.[i] / 1000 % 10
    let mode3Value = arr.[i] / 10000 % 10

    let opcode = mapOpcode opcodeValue
    let mode1 = mapParameterMode mode1Value
    let mode2 = mapParameterMode mode2Value
    let mode3 = mapParameterMode mode3Value

    match opcode with
    | Opcode.Add -> add arr i mode1 mode2 mode3
    | Opcode.Mult -> mult arr i mode1 mode2 mode3
    | Opcode.Input -> input arr i
    | Opcode.Output -> output arr i mode1
    | Opcode.JumpIfTrue -> jumpIfTrue arr i mode1 mode2
    | Opcode.JumpIfFalse -> jumpIfFalse arr i mode1 mode2
    | Opcode.LessThan -> lessThan arr i mode1 mode2 mode3
    | Opcode.Equals -> equals arr i mode1 mode2 mode3
    | Opcode.Halt -> ()

and mapOpcode value =
    match value with
    | 1 -> Opcode.Add
    | 2 -> Opcode.Mult
    | 3 -> Opcode.Input
    | 4 -> Opcode.Output
    | 5 -> Opcode.JumpIfTrue
    | 6 -> Opcode.JumpIfFalse
    | 7 -> Opcode.LessThan
    | 8 -> Opcode.Equals
    | 99 -> Opcode.Halt
    | _ -> failwith "Invalid opcode"

and mapParameterMode value =
    match value with
    | 0 -> ParameterMode.Position
    | 1 -> ParameterMode.Immediate
    | _ -> failwith "Invalid parameter mode"

and invalidOp msg =
    failwith <| "Invalid operation: " + msg

let Run () =
    let input = File.ReadAllText("day05/p.in")
                |> fun s -> s.Split(',') |> Array.map int |> Array.toList
    process input 0
