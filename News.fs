module FsAdvent.News

open System.Text.RegularExpressions

let getPage (month,day) =
    let date = System.DateTime(2005,month,day)    
    let monthName = date.ToString("MMMM").ToLower()
    let url = sprintf "http://news.bbc.co.uk/onthisday/low/dates/stories/%s/%d/default.stm"
                 monthName date.Day
    use client = new System.Net.WebClient()
    client.DownloadString(url)   

let getNewsItems html =
    let pattern = """<a href="([^"]*)"><span class="h1">(.*?)</span></a><br>(.*?)<br clear="ALL">"""
    let matches = Regex.Matches(html, pattern, RegexOptions.Singleline)
    let newsItems = [for m in matches -> [for i in 1..m.Groups.Count-1 -> m.Groups.[i].Value]]
    [for newsItem in newsItems do
        match newsItem with
        | [link;title;description] ->
            yield "http://news.bbc.co.uk" + link, title.Trim(), description.Trim().Replace("\n","").Replace("\r","")
        | _ -> ()
    ]