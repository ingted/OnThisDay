module FsAdvent.News

open System.Text.RegularExpressions

let getNewsItems (month,day) =
    let date = System.DateTime(2005,month,day)    
    let monthName = date.ToString("MMMM").ToLower()
    let url = sprintf "http://news.bbc.co.uk/onthisday/low/dates/stories/%s/%d/default.stm"
                 monthName date.Day
    use client = new System.Net.WebClient()
    let html = client.DownloadString(url)
    let pattern = """<a href="([^"]*)"><span class="h1">(.*?)</span></a><br>(.*?)<br clear="ALL">"""
    let matches = Regex.Matches(html, pattern, RegexOptions.IgnoreCase ||| RegexOptions.Singleline)
    let facts = [for m in matches -> [for i in 1..m.Groups.Count-1 -> m.Groups.[i].Value]]
    [for parts in facts do
        match parts with
        | [link;title;description] ->
            yield "http://news.bbc.co.uk" + link,
                   title.Trim(), description.Trim().Replace("\n","").Replace("\r","")
        | _ -> ()
    ]