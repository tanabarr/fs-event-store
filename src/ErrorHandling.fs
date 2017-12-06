[<AutoOpen>]
module ErrorHandling

type Result<'TResult, 'TError> =
    | Success of 'TResult
    | Failure of 'TError

let ok x = Success x
let fail x = Failure x
