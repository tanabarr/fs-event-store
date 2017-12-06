// namespace DeviceAggregator
module AgentHelper

type Agent<'T> = MailboxProcessor<'T>
let post (agent:Agent<'T>) message = agent.Post message
let postAsyncReply (agent:Agent<'T >) messageConstr = agent.PostAndAsyncReply(messageConstr)

// let agent = Agent.Start(fun (inbox:Agent<string>) ->
    // let rec loop() =
        // async {
            // let! msg = inbox.Receive()
            // match msg with
            // | "" ->
                // printfn "Stopping agent"
            // | _ ->
                // printfn "Message recieved: %s" msg
                // return! loop()
        // }
    // loop()
// )

type StreamId = StreamId of int
type StreamVersion = StreamVersion of int
type SaveResult =
    | Ok
    | VersionConflict
type Messages<'T> =
    | GetEvents of StreamId * AsyncReplyChannel<'T list option>
    | SaveEvents of StreamId * StreamVersion * 'T list * AsyncReplyChannel<SaveResult>
    | AddSubscriber of string * (StreamId * 'T list -> unit)
    | RemoveSubscriber of string
