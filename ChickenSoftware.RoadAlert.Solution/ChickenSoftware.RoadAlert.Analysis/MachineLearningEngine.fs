namespace ChickenSoftware.RoadAlert.Analysis

open FSharp.Data
open Microsoft.FSharp.Data.TypeProviders
open Accord.MachineLearning

type roadAlert2 = JsonProvider<"http://chickensoftware.com/roadalert/api/trafficstopsearch/Sample">
type MachineLearningEngine =
    static member RoadAlertDoc = roadAlert2.Load("http://chickensoftware.com/roadalert/api/trafficstopsearch")

    static member NumberOfRecords =
        MachineLearningEngine.RoadAlertDoc 
            |> Seq.length

    static member BaseDataSet =
        MachineLearningEngine.RoadAlertDoc
            |> Seq.filter(fun  x -> x.DispositionId = 7 || x.DispositionId = 15)
            |> Seq.map(fun x -> x.Id, x.StopDateTime, x.Latitude, x.Longitude, x.DispositionId)
            |> Seq.map(fun (a,b,c,d,e) -> a, b, System.Math.Round(c,3), System.Math.Round(d,3), e)
            |> Seq.map(fun (a,b,c,d,e) -> a, b, c.ToString() + ":" + d.ToString(), e)
            |> Seq.map(fun (a,b,c,d) -> a,b,c, match d with
                                                |7 -> 0
                                                |15 -> 1
                                                |_ -> 1)
            |> Seq.map(fun (a,b,c,d) -> a, b.Hour, b.DayOfWeek.GetHashCode(), b.Day, b.Month, c, d)
            |> Seq.toList

    static member TrainingSample =
        let midNumber = MachineLearningEngine.NumberOfRecords/ 2
        MachineLearningEngine.BaseDataSet
            |> Seq.filter(fun (a,b,c,d,e,f,g) -> a < midNumber)
            |> Seq.toList

    static member ValidationSample =
        let midNumber = MachineLearningEngine.NumberOfRecords/ 2
        MachineLearningEngine.BaseDataSet
            |> Seq.filter(fun (a,b,c,d,e,f,g) -> a > midNumber)
            |> Seq.toList

    static member TrainingInputClass =
        MachineLearningEngine.TrainingSample
            |> Seq.map(fun (a,b,c,d,e,f,g) -> b,c,d,e)
            |> Seq.toArray

    static member TrainingOutputClass =
        MachineLearningEngine.TrainingSample
            |> Seq.map(fun (a,b,c,d,e,f,g) -> g)
            |> Seq.toArray

    static member ValidationClass =
        MachineLearningEngine.ValidationSample
            |> Seq.map(fun (a,b,c,d,e,f,g) -> b,c,d,e,g)
            |> Seq.toArray

    static member RunKNN inputs outputs input =
        let distanceFunction (a:int,b:int,c:int,d:int) (e:int,f:int,g:int,h:int) =  
          let b1 = b * 4
          let f1 = f * 4
          let d1 = d * 2
          let h1 = h * 2
          float((pown(a-e) 2) + (pown(b1-f1) 2) + (pown(c-g) 2) + (pown(d1-h1) 2))

        let distanceDelegate = 
              System.Func<(int * int * int * int),(int * int * int * int),float>(distanceFunction)
        
        let knn = new KNearestNeighbors<int*int*int*int>(10,2,inputs,outputs,distanceDelegate)
        knn.Compute(input)
        
    static member GetValidationsViaKKN  =
        let inputs = MachineLearningEngine.TrainingInputClass
        let outputs = MachineLearningEngine.TrainingOutputClass
        let validations = MachineLearningEngine.ValidationClass

        validations
            |> Seq.map(fun (a,b,c,d,e) -> e, MachineLearningEngine.RunKNN inputs outputs (a,b,c,d))
            |> Seq.toList

    static member GetSuccessPercentageOfValidations =
        let validations = MachineLearningEngine.GetValidationsViaKKN
        let matches = validations
                        |> Seq.map(fun (a,b) -> match (a=b) with
                                                    | true -> 1
                                                    | false -> 0)

        let recordCount =  validations |> Seq.length
        let numberCorrect = matches |> Seq.sum
        let successPercentage = double(numberCorrect) / double(recordCount)
        recordCount, numberCorrect, successPercentage

//TODO
//    static member LargestClassInTrainingSample =
//        MachineLearningEngine.TrainingSample
//            |> Seq.groupBy(fun (a,b,c,d,e,f,g) -> a,b,c,d,e,f,g)
//            |> Seq.toList
//            |> List.rev
//            |> Seq.take 1
//            |> Seq.toList
//
//    static member LaregestTrainingInputClass =
//        MachineLearningEngine.LargestClassInTrainingSample
//            |> Seq.map(fun (a,b) -> b,c,d,e)
//            |> Seq.toArray
//
//    static member LargestTrainingOutputClass =
//        MachineLearningEngine.LargestClassInTrainingSample
//            |> Seq.map(fun (a,b,c,d,e,f,g) -> g)
//            |> Seq.toArray
//
//
//    static member GetValidationsForLargestClassViaKKN  =
//        let inputs = MachineLearningEngine.TrainingInputClass
//        let outputs = MachineLearningEngine.TrainingOutputClass
//        let validations = MachineLearningEngine.ValidationClass
//
//        validations
//            |> Seq.map(fun (a,b,c,d,e) -> e, MachineLearningEngine.RunKNN inputs outputs (a,b,c,d))
//            |> Seq.toList

    
            
//(inputs:(int*int*int*int)[]) (outputs:(int)[]) (validations:(int*int*int*int*int*int)[])