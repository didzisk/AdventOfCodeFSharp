module Utils.Downloader

open System
open System.Net
open System.Net.Http

let downloadInput (dir:string) =
    let cookieContainer = new CookieContainer();
    use handler = new HttpClientHandler(CookieContainer = cookieContainer)
    use c = new HttpClient(handler)
    let sess = "53616c7465645f5f4545bae591f3184fdb136bd7829f24293bdfb611920340de631588d174746123fa5775a4009529e169a310a12426caaa5e919e0f997aefcf"
    cookieContainer.Add(new Uri("https://adventofcode.com"), Cookie("session", sess))

    let day = Int32.Parse(dir.Substring(dir.Length-2))
    let url = $"https://adventofcode.com/2023/day/{day}/input"
    
    let inputFile = dir + "\input.txt"

    async {
        let! bytes = c.GetByteArrayAsync(url) |> Async.AwaitTask
        System.IO.File.WriteAllBytes(inputFile, bytes)
    }
    |> Async.RunSynchronously
