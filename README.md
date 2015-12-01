# OnThisDay
F# Type Provider that provides a set of news items for today sourced from the [BBC news archives](http://news.bbc.co.uk/onthisday/bsp/about_this_site.stm).

## Sample

```F#
#r "./bin/debug/FsAdvent.OnThisDay.dll"

FsAdvent.OnThisDay.``1990: Tunnel links UK and Europe``
|> System.Diagnostics.Process.Start
```

## Disclaimer

This is sample code to demonstrate how to build a simple F# type provider and not intended for use in the enterprise.
