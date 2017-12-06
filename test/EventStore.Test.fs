module Test.EventStore

open AgentHelper
open EventStore
open Fable.Import.Jest
open Matchers
open Fable.PowerPack

test "running the event store" <| fun () ->
  let inMemoryEventStore = createInMemoryEventStore<string,string> "This is a version error"

  inMemoryEventStore.AddSubscriber "FirstSubscriber" (printfn "%A")

  let res0 = inMemoryEventStore.SaveEvents (StreamId 1) (StreamVersion 0) ["Hello";"World"]
  let res1 = inMemoryEventStore.SaveEvents (StreamId 1) (StreamVersion 1) ["Hello2";"World2"]
  let res2 = inMemoryEventStore.SaveEvents (StreamId 1) (StreamVersion 2) ["Hello2";"World2"]

  toBe ([res0;res1;res2] |> List.mapi (fun i v -> printfn "%i: %A" i v)) ""

///// Can use jest snapshots
///// To derive results
//test "testing snapshots" <| fun () ->
//    toMatchSnapshot (add 1 2)
//
///// Can use matchers to simulate
///// fn dependencies.
///// The mock is statically checked.
//test "testing mocks" <| fun () ->
//  let m = Matcher<int, int>(fun x -> x + 1)
//
//  m.Mock 1 === 2
//
//  m.CalledWith 1
//
//  m <?> 1 // Can use custom operator instead of method call.
//
///// Can use a testList like expecto
//testList "Testing the testList" [
//  Test("adding", fun () -> 1 === 1);
//  TestDone("waiting", fun (x) ->
//    1 === 1
//
//    x.``done``()
//  );
//  TestAsync("promises", fun () ->
//    promise {
//      1 === 1
//    }
//  )
//]
//
///// Works with fixtures as well.
//testList "testFixtures" [
//  let fixture fn () =
//    fn(+)
//
//  yield! testFixture fixture [
//    "add some stuff", fun (op) -> op 1 2 === 3
//  ]
//
//  let doneFixture fn (x:Bindings.DoneStatic) =
//    fn(-)
//
//    x.``done``()
//
//  yield! testFixtureDone doneFixture [
//    "subtract some stuff", fun (op) -> op 2 1 === 1
//  ]
//
//  let asyncFixture fn () =
//    promise {
//      let! op = Promise.lift(*)
//
//      fn(op)
//    }
//
//  yield! testFixtureAsync asyncFixture [
//    "multiply some stuff", fun (op) -> op 3 2 === 6
//  ]
//]
