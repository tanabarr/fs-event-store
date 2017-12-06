module Main

open AgentHelper

/// This code is covered by tests.
let add a b = a + b
/// This code is not.
let sub a b = a - b
let agent = Agent.Start(fun (inbox:Agent<string>) ->
    let rec loop() =
        async {
            let! msg = inbox.Receive()
            match msg with
            | "" ->
                printfn "Stopping agent"
            | _ ->
                printfn "Message recieved: %s" msg
                return! loop()
        }
    loop()
)

"hello" |> post agent
"" |> post agent
"hello" |> post agent
