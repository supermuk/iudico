<# 
.SYNOPSIS 
    Stylecop HTML report generator. 
    
.DESCRIPTION 
    Creates StyleCopViolationsReport.html from FullStyleCopViolations.xml in the current directory 
    These are reports with pretty reporting
        
.NOTES 
    File Name  : GenerateStyleCopReport.ps1 
    Requires   : PowerShell Version 2.0
#> 
function Insert-Newline([System.Xml.XmlElement] $element) {
    $break = $element.OwnerDocument.CreateSignificantWhitespace("`r`n");
    $element.AppendChild($break) | Out-Null
}

function Make-HTML([string] $file) {
    Write-Host "Processing report $file"
    $htmlfile = $file.Replace("FullStyleCopViolations.xml", "CodeStyleReport.html")
    [xml]$raw = Get-Content $file
    [xml]$report = @"
<!DOCTYPE html>
<html>
<head>
<title>IUDICO Code Style Report</title>
<script src="jquery.min.js"></script>
<style>
body {position: absolute; top: 0px; width: 100%;margin:0px;min-height:100%;padding:0}
body {font-family:Verdana, Helvetica, sans-serif;font-size:70%;background:#fff;color:#000;}
h1 { padding: 10px; width: 100%; background-color: #566077; color: #fff; }
h2 { margin:10px; padding: 10px; background-color: #ffc; border: #d7ce28 1px solid;  }
h3 { margin:25px; padding: 10px; background: #b9c9fe; color: #039; border: 1px solid #aabcfe; }
h3 span {float: right;}
div { margin:25px; border: #d7ce28 1px solid; }
div div { border: none; }
pre { font-family:Consolas, Inconsolata, "Lucida Console", "Courier New", monospace;font-size:100%; padding: 0; margin:0}
pre.odd { background: #eee; }
pre.even { background: #ddf; }
span.violation { background: #ff8; }
span.index { border-right : 1px solid black }
</style>
<script type="text/javascript">
`$(function(){
  // inner first, then outer
  `$('span.violation')
    .click(function(event) {
      if (this == event.target) {
        `$(this).parent().next().toggle();
      }
    return false;
  })
  .css('cursor', 'pointer')
  .click();  
  
  `$('h3')
    .click(function(event) {
      if (this == event.target) {
        `$(this).next().toggle();
      }
    return false;
  })
  .css('cursor', 'pointer')
  .click();
});
</script>

</head>

<body>
<h1>IUDICO Code Style Report</h1>

</body>

</html>
"@
    $body = (Select-Xml -Xml $report -XPath "//body").Node
    $doc = $body.OwnerDocument
    $doc.PreserveWhitespace = $true
    Insert-Newline $body
    
    $x = $doc.CreateElement("h2", "")
    $violations = (Select-Xml -Xml $raw -XPath "//Violation")
    
    $x.InnerText = "Total Violations : $($violations.Length)"
    $body.appendChild($x) | Out-Null
    Insert-Newline $body
    
    $vfiles = @($violations | % { $_.Node.Source } | Sort-Object -Unique)
    
    $vfiles | % { 
        $path = Resolve-Path $_
        $local = (Select-Xml -Xml $raw -XPath "//Violation[@Source='$_']")

        $h = $doc.CreateElement("h3", "")
        $body.AppendChild($h) | Out-Null
        Insert-Newline $body
        
        $t = $doc.CreateTextNode("SourceFile: $path")
        $span = $doc.CreateElement("span", "")
        
        $span.InnerText = "($($local.Length) violations)"
        $h.AppendChild($t)     | Out-Null
        $h.AppendChild($span)  | Out-Null
        
        $sdiv = $doc.CreateElement("div", "")
        $body.AppendChild($sdiv) | Out-Null
                    
        $source = Get-Content $path
        
        $line = 0
        $source | % {
            $line += 1
            $pre = $doc.CreateElement("pre", "")
            $num = $line.ToString()
            while ($num.Length -lt 4) { $num = " " + $num }
            $span1 = $doc.CreateElement("span", "")
            $span1.InnerText = "$num "
            $span1.SetAttribute("class", "index")
            $pre.AppendChild($span1) | Out-Null
            
            $span2 = $doc.CreateElement("span", "")
            $span2.InnerText = $_
            $pre.AppendChild($span2) | Out-Null
            
            $sdiv.AppendChild($pre) | Out-Null
            $pre.SetAttribute("class", "odd")
            if (0 -eq ($line % 2)) { $pre.SetAttribute("class", "even") }
            
            $localhere = @($local | % { $_.Node } | ? { $_.LineNumber -eq $line })
            if ($localhere.Length -gt 0) { 
                $span2.SetAttribute("class", "violation")
                $pdiv = $doc.CreateElement("div", "")
                Insert-Newline $sdiv
                $sdiv.AppendChild($pdiv) | Out-Null
                $ul = $doc.CreateElement("ul", "")
                $pdiv.AppendChild($ul) | Out-Null
                $localhere | % {
                    $li = $doc.CreateElement("li", "")
                    $li.InnerText = $_.InnerText
                    $ul.AppendChild($li) | Out-Null
                    Insert-Newline $ul
                }
            }
            
            Insert-Newline $sdiv
        }
    } # $vfiles
    
    $report.Save($htmlfile)
} # function

$files = @(dir FullStyleCopViolations.xml)
$files | % { Make-HTML $_.FullName }