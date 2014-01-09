namespace ChickenSoftware.RoadAlert.Analysis

open FSharp.Data
open Microsoft.FSharp.Data.TypeProviders

//#r "C:/TFS/ChickenSoftware.RoadAlertServices.Solution/packages/FSharp.Data.1.1.10/lib/net40/FSharp.Data.dll"

//JSON
//http://localhost:17680/api/trafficstopsearch/Sample
//http://chickensoftware.com/roadalert/api/trafficstopsearch/Sample

type roadAlert = JsonProvider<"http://chickensoftware.com/roadalert/api/trafficstopsearch/Sample">
type AnalysisEngine =
    static member RoadAlertDoc = roadAlert.Load("http://chickensoftware.com/roadalert/api/trafficstopsearch")

    static member NumberOfRecords =
        AnalysisEngine.RoadAlertDoc 
            |> Seq.length
        
    static member ActualTrafficStopsByMonth =
        AnalysisEngine.RoadAlertDoc
            |> Seq.map(fun x -> x.StopDateTime.Month)
            |> Seq.countBy(fun x-> x)
            |> Seq.toList

    static member Months =
        let monthList = [1..12]
        Seq.map (fun x -> 
                match x with
                    | 1 | 3 | 5 | 7 | 8 | 10 | 12 -> x,31,31./365.
                    | 2 -> x,28,28./365.
                    | 4 | 6 | 9 | 11 -> x,30, 30./365.
                    | _ -> x,0,0.                    
            ) monthList
        |> Seq.toList         


    static member ExpectedTrafficStopsByMonth numberOfStops =
        AnalysisEngine.Months
            |> Seq.map(fun (x,y,z) -> 
                x, int(z*numberOfStops))
            |> Seq.toList

    static member TrafficStopsByMonth =
        let numberOfStops = float(AnalysisEngine.NumberOfRecords)
        let monthlyExpected = AnalysisEngine.ExpectedTrafficStopsByMonth numberOfStops
        let monthlyActual = AnalysisEngine.ActualTrafficStopsByMonth
        Seq.zip monthlyExpected monthlyActual
            |> Seq.map(fun (x,y) -> fst x, snd x, snd y, snd y - snd x, (float(snd y) - float(snd x))/float(snd x))
            |> Seq.toList
            
    
    static member ActualTrafficStopsByDay = 
        AnalysisEngine.RoadAlertDoc
            |> Seq.map(fun x -> x.StopDateTime.Day)
            |> Seq.countBy(fun x-> x)
            |> Seq.toList

    static member Days =
        let dayList = [1..31]
        Seq.map (fun x -> 
                match x with
                    | x when x < 29 -> x, 12, 12./365.
                    | 29 | 30 -> x, 11, 11./365.
                    | 31 -> x, 7, 7./365.
                    | _ -> x, 0, 0.                 
            ) dayList
        |> Seq.toList     

    
    static member ExpectedTrafficStopsByDay numberOfStops =
        AnalysisEngine.Days
            |> Seq.map(fun (x,y,z) -> 
                x, int(z*numberOfStops))
            |> Seq.toList    

    static member TrafficStopsByDay =
        let numberOfStops = float(AnalysisEngine.NumberOfRecords)
        let dailyExpected = AnalysisEngine.ExpectedTrafficStopsByDay numberOfStops
        let dailyActual = AnalysisEngine.ActualTrafficStopsByDay
        Seq.zip dailyExpected dailyActual
            |> Seq.map(fun (x,y) -> fst x, snd x, snd y, snd y - snd x, (float(snd y) - float(snd x))/float(snd x))
            |> Seq.toList

    static member ActualTrafficStopsByHour = 
        AnalysisEngine.RoadAlertDoc
            |> Seq.map(fun x -> x.StopDateTime.Hour)
            |> Seq.countBy(fun x-> x)
            |> Seq.toList

    static member Hours =
        let hourList = [1..24]
        Seq.map (fun x -> 
                    x,1, 1./24.
            ) hourList
        |> Seq.toList     

    static member ExpectedTrafficStopsByHour numberOfStops =
        AnalysisEngine.Hours
            |> Seq.map(fun (x,y,z) -> 
                x, int(z*numberOfStops))
            |> Seq.toList    

    static member TrafficStopsByHour =
        let numberOfStops = float(AnalysisEngine.NumberOfRecords)
        let hourlyExpected = AnalysisEngine.ExpectedTrafficStopsByHour numberOfStops
        let hourlyActual = AnalysisEngine.ActualTrafficStopsByHour
        Seq.zip hourlyExpected hourlyActual
            |> Seq.map(fun (x,y) -> fst x, snd x, snd y, snd y - snd x, (float(snd y) - float(snd x))/float(snd x))
            |> Seq.toList

    static member GetVarianceOfTrafficStopsDifferenceByHour =
        let trafficStopList = AnalysisEngine.TrafficStopsByHour
                                |> Seq.map(fun (v,w,x,y,z) -> double(y))
                                |> Seq.toList
        AnalysisEngine.Variance(trafficStopList)

    static member ActualTrafficStopsByGPS =  
        AnalysisEngine.RoadAlertDoc
            |> Seq.map(fun x -> System.Math.Round(x.Latitude,3).ToString() + ":" + System.Math.Round(x.Longitude,3).ToString())
            |> Seq.countBy(fun x-> x)
            |> Seq.sortBy snd
            |> Seq.toList
            |> List.rev

    static member GetVarianceOfTrafficStopsByGPS =
        let trafficStopList = AnalysisEngine.ActualTrafficStopsByGPS
                                |> Seq.map(fun x -> double(snd x))
                                |> Seq.toList
        AnalysisEngine.Variance(trafficStopList)

    static member GetAverageOfTrafficStopsByGPS =
        AnalysisEngine.ActualTrafficStopsByGPS
            |> Seq.map(fun x -> double(snd x))
            |> Seq.average


    static member Variance (source: List<double>) =
        let mean = Seq.average source
        let deltas = Seq.map(fun x -> pown(x-mean) 2) source
        Seq.average deltas
    





//OData
//http://chickensoftware.com/roadalert/odata
//http://localhost:17680/odata
//type roadAlert = ODataService<"http://chickensoftware.com/roadalert/odata">
//let db = Northwind.GetDataContext()
//let fullContext = Northwind.ServiceTypes.NorthwindEntities()

