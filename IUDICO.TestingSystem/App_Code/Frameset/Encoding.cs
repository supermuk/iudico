// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Encoding.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

#endregion

namespace Microsoft.LearningComponents.Frameset
{
    /**

    <summary>
    [Internal class?]
    Represents a series of Unicode characters as an end user would see them,
    without any encoding or markup.
    </summary>

    <remarks>
    TODO
    </remarks>

    <seealso cref="HtmlString" />
    <seealso cref="JScriptString" />
    <seealso cref="UrlString" />
    <seealso cref="HtmlStringWriter" />

    */

    [DebuggerDisplay("{mPlaintext}")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")] // PlainText is ok as is
    public class PlainTextString
    {
        // see TODO#1 above
        /// <summary>
        /// The string value wrapped by this class.
        /// </summary>
        private string mPlaintext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextString"/> class.
        /// </summary>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public PlainTextString()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextString"/> class
        /// and specifies initial text for the object to contain.
        /// </summary>
        ///
        /// <param name="plaintext">Initial value of the object.</param>
        public PlainTextString(string plaintext)
        {
            this.mPlaintext = plaintext;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextString"/> class
        /// and specifies initial text for the object to contain.
        /// </summary>
        ///
        /// <param name="htmlText">Initial value of the object.</param>
        ///
        /// <remarks>
        /// The <P>htmlText</P> will be converted to plain text by a call to <Mth>HttpUtility.HtmlDecode</Mth>.
        /// </remarks>
        ///
        public PlainTextString(HtmlString htmlText)
        {
            this.mPlaintext = HttpUtility.HtmlDecode(htmlText);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public override string ToString()
        {
            return this.mPlaintext;
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlString ToHtmlString()
        {
            return new HtmlString(this);
        }

        /// <summary>
        /// TODO 
        /// </summary>
        /// 
        /// <param name="plaintext">TODO</param>
        /// 
        /// <remarks>
        /// TODO
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is validated
        public static implicit operator string(PlainTextString plaintext)
        {
            FramesetUtil.ValidateNonNullParameter("plaintext", plaintext);
            return plaintext.mPlaintext;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="plaintext">TODO</param>
        /// 
        /// <remarks>
        /// TODO
        /// </remarks>
        /// 
        public static implicit operator PlainTextString(string plaintext)
        {
            return new PlainTextString(plaintext);
        }
    }

    /**

    <summary>
    [Internal class?] Represents HTML markup, i.e. a series of Unicode characters
    intended to be written to an HTML file and read by a Web browser.
    </summary>

    <remarks>

    <Typ>/System.String</Typ> can be used to represent many kinds of strings of
    Unicode characters: plain text, HTML markup, URLs, etc.  While that flexibility
    is convenient, it comes at a cost: it's too easy to pass the wrong type of
    string from one piece of code to another (for example, to a method from
    its caller).  This can lead to incorrect results, depending on the input
    data, and may even result in security bugs such as script insertion
    vulnerabilities.  Bugs can be caused by (a) a developer not understanding
    all the subtleties of string encoding, and/or (b) misunderstanding a method's
    "contract" (i.e. comments defining legal inputs, outputs, and side effects).

    <p/>

    The following simple example (a helper method in a C# ASP.NET application)
    illustrates both problems:

    <code language="C#">
    /// &lt;summary&gt;
    /// Outputs HTML consisting of an anchor element which, when clicked,
    /// displays given text within a JScript alert.
    /// &lt;/summary&gt;
    ///
    /// &lt;param name="linkText"&gt;Text of the link.&lt;/param&gt;
    ///
    /// &lt;param name="alertText"&gt;Text to display in the alert.&lt;/param&gt;
    ///
    void WriteAlertLink(string linkText, string alertText)
    {
        Response.Write(String.Format("&lt;a href=\"javascript:alert('{0}')\"&gt;{1}&lt;/a&gt;",
            alertText, linkText));
    }
    </code>

    Calling the function as follows:

    <code language="C#">
    WriteAlertLink("Click me!", "Thanks for clicking me.");
    </code>

    will generate a "<u>Click me!</u>" link which, when clicked, correctly displays
    the following:

    <Img>WriteAlertLink_1.gif</Img>

    So far so good.

    <p/>

    Unfortunately, calling the method as follows:

    <code language="C#">
    WriteAlertLink("Select&amp;copy", @"See C:\100%BEEF\*.txt");
    </code>

    generates an odd-looking link "<u>Select&#169;</u>" which, when clicked,
    displays the following:

    <Img>WriteAlertLink_2.gif</Img>

    Worse yet, calling the method as follows:

    <code language="C#">
    WriteAlertLink("Oops", "That's all, folks!");
    </code>

    generates a script error:

    <Img>WriteAlertLink_3.gif</Img>

    How can there be so many problems in such a simple one-line method?
    <c>WriteAlertLink</c> has two parameters, and both have problems.  The
    first&#8212;and easiest to fix&#8212;is <c>linkText</c>.  Any good developer
    familiar with HTML can spot the problem here: <c>linkText</c> is not
    HTML-encoded.  A more precise way to think about the problem is that there is a
    kind of type mismatch: in the examples above, the caller is expecting that
    <c>linkText</c> is of type "plain text", but in fact <c>linkText</c> is of type
    "HTML".  To fix this problem,

    <ul>

    <li><u>either</u> the method contract (header comments) need to change to
    reflect the fact that the <c>linkText</c> is of type "HTML" (in which case it
    should probably be named <c>linkHtml</c>, not <c>linkText</c>),</li>

    <li><u>or</u> a method such as <c>HttpUtility.HtmlEncode</c> needs to be used
    to convert <c>linkText</c> from type "HTML" to type "plain text".  (In this
    case, the contract comments should also be updated to clarify that
    <c>linkText</c> is of type "HTML".)</li>

    </ul>

    Parameter <c>alertText</c> is more problematic.  If the effective type of
    <c>linkText</c> is HTML, what's the effective type of alertText?  To answer
    that question, recall that <c>WriteAlertLink</c> generates the following HTML:

    <p>
    <pre>&lt;a href="javascript:alert('<i>alertText</i>')"&gt;<i>linkText</i>&lt;/a&gt;</pre>
    </p>

    Now, examine what will happen to <c>alertText</c> at runtime, before it is
    passed to the <c>alert</c> function:

    <ol>

    <li>When the browser parses the "href" attribute, converts character
    entity references such as "&amp;#32;" and "&amp;quot;" into corresponding
    characters.</li>

    <li>When the user clicks on the link, the browser notices that the href's
    protocol is "javascript:", so it:

        <ol type="a">

        <li>converts any "%xx" hexadecimal escape sequences within
        "javascript:"...  into the corresponding characters, and</li>

        <li>passes everything after "javascript:" to the JScript
        interpreter.</li>

        </ol>

    </li>

    <li>The JScript interpreter sees a single-quoted string
    (<c>'<i>alertText</i>'</c>) and it:

        <ol type="a">

        <li>converts any backslash escape sequences such as "\n" and "\007"
        to the corresponding characters, and</li>

        <li>generates a script error if there is another single quote (') within
        <c>alertText</c>.</li>

        </ol>

    </li>

    </ol>

    Consider the example where <c>alertText</c> is "See C:\100%BEEF\*.txt" as shown
    in one of the examples above:

    <ul>

    <li>#1 doesn't apply&#8212;there are no character entity references in this
    string.</li>

    <li>#2 causes "%BE" in the string to change to "&#190;", since Unicode
    character 0x00BE is "&#190;".  The result is "See C:\100&#190;EF\*.txt".
    </li>

    <li>#3 causes "\100" to change to "@", since octal 100 (decimal 64) is the
    character code for "@".  Also, "\*" changes to just "*"&#8212;that's what
    JScript does to unknown backslash escape sequences.  The result is
    "See C:@&#190;EF*.txt".</li>

    </ul>

    In the case where <c>alertText</c> is "That's all, folks!", #3 causes a script
    error due to the embedded single quote.

    <p/>

    Microsoft.LearningServer.Library addresses the string encoding problem by
    introducing several classes that serve as "wrappers" for specific kinds of
    strings:

    <ol>

    <li><Typ>plaintextString</Typ> represents text without any encoding or markup,
    as an end user would see it.</li>

    <li><Typ>HtmlString</Typ> represents HTML markup, intended to be interpreted
    by a Web browser.</li>

    <li><Typ>JScriptString</Typ> represents JScript source code that's intended
    to be written to a &lt;script&gt;...&lt;/script&gt; block within an HTML file.
    </li>

    <li><Typ>UrlString</Typ> represents a string that can be typed into the address
    bar of a Web browser.</li>

    </ol>

    These classes can be used in places where you want to refer to a more specific
    kind of string.  For example, assume the following method is defined:

    <code language="C#">
    void Foo(HtmlString html)
    {
        Console.WriteLine(html);
    }
    </code>

    This method simply copies the HTML markup within the <c>html</c> parameter
    to the console.  (Note that this involes an implicit conversion from
    <Typ>HtmlString</Typ> to <Typ>/System.String</Typ>&#8212;that conversion
    returns the HTML markup as-is.)  Since the argument is of type
    <Typ>HtmlString</Typ>, the following call generates a compiler error:
    "cannot convert from 'string' to 'HtmlString'".

    <code language="C#">
    Foo("Select&amp;copy"); // compiler error
    </code>

    Instead, the caller needs to be more explicit about the type of string
    being passed in.  The following two lines produce the same output; in the
    first case, the string begins as a plaintextString that is converted to
    an HtmlString using <Mth>../plaintextString.ToHtmlString</Mth>; in the second
    case the string begins as HTML markup.

    <code language="C#">
    Foo(new plaintextString("Select&amp;copy").ToHtmlString());
    Foo(new HtmlString("Select&amp;amp;copy"));
    </code>

    Defining the argument of <c>Foo</c> as <Typ>HtmlString</Typ> instead of
    simply <Typ>/System.String</Typ> has the following advantages:

    <ol>

    <li>The argument is clearly documented as being an HTML string.</li>

    <li>Certain encoding errors (such as passing a <Typ>plaintextString</Typ>
    or <Typ>/System.String</Typ> value) can be caught at compile time.</li>

    </ol>

    Returning to the <c>WriteAlertLink</c> example, the following is a more
    type-safe version of that method that uses some of the classes in the
    Microsoft.LearningServer.Library namespace:

    <code language="C#">
    void WriteAlertLink(HtmlString linkHtml, plaintextString alertText)
    {
        HtmlStringWriter htmlWriter = new HtmlStringWriter(Response.Output);
        htmlWriter.AddJavascriptProtocolAttribute(HtmlTextWriterAttribute.Href,
            new JScriptString(String.Format("alert({0})",
                JScriptString.QuoteString(alertText, true))));
        htmlWriter.RenderBeginTag(HtmlTextWriterTag.A);
        htmlWriter.WriteHtml(linkHtml);
        htmlWriter.RenderEndTag();
    }
    </code>

    In addition to using <Typ>plaintextString</Typ>, <Typ>HtmlString</Typ>, and
    <Typ>JScriptString</Typ>, the example above demonstrates another
    Microsoft.LearningServer.Library encoding helper class,
    <Typ>HtmlStringWriter</Typ>, which is similar to
    <Typ>/System.Web.UI.HtmlTextWriter</Typ> but is more type-safe.
    See the individual classes used above for more information.

    <p/>

    The new version of <c>WriteAlertLink</c> shown above is called as follows:

    <code language="C#">
    WriteAlertLink(new HtmlString("Select&amp;amp;copy"),
        new plaintextString(@"See C:\100%BEEF\*.txt"));
    </code>

    As expected, this generates a "<u>Select&amp;copy</u>" hyperlink which, when
    clicked, displays the following:

    <Img>WriteAlertLink_4.gif</Img>

    You would get the same results by calling <c>WriteAlertLink</c> as follows:

    <code language="C#">
    WriteAlertLink(new plaintextString("Select&amp;copy").ToHtmlString(),
        new plaintextString(@"See C:\100%BEEF\*.txt"));
    </code>

    In fact, since an implicit conversion is defined between
    <Typ>/System.String</Typ> and <Typ>plaintextString</Typ>, the following
    also works:
    <code language="C#">
    WriteAlertLink(new plaintextString("Select&amp;copy").ToHtmlString(),
        @"See C:\100%BEEF\*.txt");
    </code>

    However, the following would generate a compiler error, since there is
    no implicit conversion between <Typ>/System.String</Typ> and
    <Typ>HtmlString</Typ>:

    <code language="C#">
    WriteAlertLink("Select&amp;copy", @"See C:\100%BEEF\*.txt"); // compiler error
    </code>

    </remarks>

    <seealso cref="PlainTextString" />
    <seealso cref="JScriptString" />
    <seealso cref="UrlString" />
    <seealso cref="HtmlStringWriter" />

    */

    [DebuggerDisplay("{mHtml}")]
    public class HtmlString
    {
        // see TODO#1 above
        /// <summary>
        /// The string value wrapped by this class.
        /// </summary>
        ///
        private string mHtml;

        /// <summary>
        /// Represents a string containing HTML.
        /// </summary>
        /// 
        /// <param name="html">Html to initialize the representation.</param>
        public HtmlString(string html)
        {
            this.mHtml = html;
        }

        /// <summary>
        /// Convert a plaintextString to Html.
        /// </summary>
        /// <param name="plaintext">Text string.</param>
        /// <remarks>
        /// HtmlEncodes the <paramref name="plaintext"/>
        /// </remarks>
        public HtmlString(PlainTextString plaintext)
        {
            this.mHtml = HttpUtility.HtmlEncode(plaintext);
        }

        /// <summary>
        /// Returns the Html associated with this string.
        /// </summary>
        /// 
        /// <returns>
        /// The string containing html.
        /// </returns>
        public override string ToString()
        {
            return this.mHtml;
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="html">TODO</param>
        ///
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public static implicit operator string(HtmlString html)
        {
            if (html == null)
            {
                return null;
            }
            return html.mHtml;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="html">TODO</param>
        /// 
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public static explicit operator HtmlString(string html)
        {
            return new HtmlString(html);
        }
    }

    /**

    <summary>
    [Internal class?]
    Represents JScript source code, i.e. a series of Unicode characters
    intended to be written into a &lt;script&gt;...&lt;/script&gt; block of
    an HTML file.
    </summary>

    <remarks>

    TODO...

    <p/>

    Note that <Typ>JScriptString</Typ> should not be used to represent a
    "javascript:" URI.  For example, if you place "alert('100%BEEF')"
    within a &lt;script&gt;...&lt;/script&gt; block, you'll get an alert
    with the message "100%BEEF".  However, in "javascript:alert('100%BEEF')"
    "%BE" is interpreted as character 0xBE&#8212;if that's not what's intended,
    "javascript:alert('100%25BEEF') should be used.  Use
    <Mth>ToJavascriptProtocol</Mth> to convert from <Typ>JScriptString</Typ>
    to a "javascript:" URI.

    </remarks>

    <seealso cref="PlainTextString" />
    <seealso cref="HtmlString" />
    <seealso cref="UrlString" />
    <seealso cref="HtmlStringWriter" />

    */

    [DebuggerDisplay("{mJscript}")]
    public class JScriptString
    {
        // see TODO#1 above
        /// <summary>
        /// The string value wrapped by this class.
        /// </summary>
        private string mJscript;

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="jscript">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public JScriptString(string jscript)
        {
            this.mJscript = jscript;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="plaintext">TODO</param>
        /// 
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        /// 
        public static JScriptString QuoteString(string plaintext)
        {
            return QuoteString(plaintext, false);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="plaintext">TODO</param>
        /// 
        /// <param name="singleQuote">TODO</param>
        /// 
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters")] // singleQuote is not a single
        public static JScriptString QuoteString(string plaintext, bool singleQuote)
        {
            if (singleQuote)
            {
                return InternalQuoteString(plaintext, '\'');
            }
            else
            {
                return InternalQuoteString(plaintext, '"');
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="jscript">TODO</param>
        /// 
        /// <param name="quoteChar">TODO</param>
        /// 
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        /// 
        private static JScriptString InternalQuoteString(string jscript, char quoteChar)
        {
            if (jscript == null)
            {
                jscript = string.Empty;
            }
            jscript = jscript.Replace(@"\", @"\\");
            jscript = jscript.Replace(new string(quoteChar, 1), @"\" + quoteChar);
            return new JScriptString(quoteChar + jscript + quoteChar);
        }

        /// <summary>See <see cref="object.ToString"/>.</summary>
        public override string ToString()
        {
            return this.mJscript;
        }

        /// <summary>Converts to a javascript call.</summary>
        /// <returns>Add javascript: to start and replaces % with %25.</returns>
        public string ToJavascriptProtocol()
        {
            return "javascript:" + this.mJscript.Replace("%", "%25");
        }

        /// <summary>Implicit conversion of JScriptString to string.</summary>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is validated
        public static implicit operator string(JScriptString jscript)
        {
            FramesetUtil.ValidateNonNullParameter("jscript", jscript);
            return jscript.mJscript;
        }

        /// <summary>Explicit conversion of string to JScriptString.</summary>
        public static explicit operator JScriptString(string jscript)
        {
            return new JScriptString(jscript);
        }
    }

    /// <summary>
    /// Represents a string that can be typed into the address bar of a Web
    /// browser, or a substring of such a string.
    /// </summary>
    [DebuggerDisplay("{mUrl}")]
    public class UrlString
    {
        /// <summary>
        /// The string value wrapped by this class.
        /// </summary>
        private string mUrl;

        /// <summary>Initializes a new instance of <see cref="UrlString"/>.</summary>
        /// <param name="url">The string value representing a url.</param>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        // the class operates on strings
        public UrlString(string url)
        {
            this.mUrl = url;
        }

        /// <summary>Initializes a new instance of <see cref="UrlString"/>.</summary>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is validated
        public UrlString(Uri uri)
        {
            FramesetUtil.ValidateNonNullParameter("uri", uri);
            this.mUrl = uri.AbsoluteUri;
        }

        /// <summary>Implicitly converts a UrlString to a string.</summary>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is validated
        public static implicit operator string(UrlString url)
        {
            FramesetUtil.ValidateNonNullParameter("url", url);
            return url.mUrl;
        }

        /// <summary>Explicitly converts a string to a UrlString.</summary>
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")]
        // the class operates on strings, so creating an object so that a string can be read is not ideal
        public static explicit operator UrlString(string url)
        {
            return new UrlString(url);
        }

        /// <summary>See <see cref="object.ToString"/>.</summary>
        public override string ToString()
        {
            return this.mUrl;
        }

        /// <summary>
        /// Converts any non-ASCII characters within the string to UTF-8 and
        /// hexadecimal-encoded (%xx) equivalents.
        /// </summary>
        ///
        /// <returns>
        /// An <Typ>UrlString</Typ> with non-ASCII characters converted
        /// to UTF-8 sequences that are then hexadecimal-encoded.
        /// </returns>
        ///
        /// <remarks>
        ///
        /// If you type "http://localhost/LaNiña" into Internet Explorer, it will
        /// convert the URL internally to "http://localhost/LaNi%c3%b1a", which
        /// is an equivalent representation of the URL that only contains ASCII
        /// characters. (You can also type "http://localhost/LaNi%c3%b1a" directly
        /// into a browser&#8212;doing so will navigate to the same document.)
        ///
        /// <p/>
        ///
        /// This method converts any non-ASCII character within the string to
        /// UTF-8 and hexadecimal-encoded (%xx) equivalents.  For example,
        /// "ñ" is converted to "%c3%b1".
        ///
        /// <p/>
        /// 
        /// If the URL doesn't contain non-ASCII characters, the output of this
        /// method is the same as its input.  Example:
        ///
        /// <code language="C#">
        /// UrlString url1 = new UrlString("http://localhost/LaNiña");
        /// UrlString url2 = url1.ToAscii(); // returns "http://localhost/LaNi%c3%b1a"
        /// UrlString url3 = url2.ToAscii(); // returns "http://localhost/LaNi%c3%b1a"
        /// </code>
        ///
        /// <Mth>ToAscii</Mth> does not fully "canonicalize" the URL.  In
        /// particular, the space character is not converted to "%20".
        ///
        /// </remarks>
        ///
        /// <example>
        ///
        /// The following code:
        ///
        /// <code language="C#">
        /// Console.WriteLine(new UrlString("http://localhost/LaNiña").ToAscii());
        /// </code>
        ///
        /// writes the following output:
        ///
        /// <p>
        /// <pre>http://localhost/LaNi%c3%b1a</pre>
        /// </p>
        ///
        /// </example>
        ///
        public UrlString ToAscii()
        {
            // note that HttpUtility.UrlEncode("http://localhost/LaNiña") returns
            // "http%3a%2f%2flocalhost%2fLaNi%c3%b1a", which is not what we want
            // (that would change the meaning of the URL); instead, we look for
            // non-ASCII characters and only convert them

            // point <ich> to the first non-ASCII character in <mUrl>; if none is
            // found, no conversion is required
            int ich = 0;
            while (true)
            {
                if (ich >= this.mUrl.Length)
                {
                    return this; // no non-ASCII characters, so no conversion needed
                }
                if (this.mUrl[ich] > '\x7F')
                {
                    break;
                }
                ich++;
            }

            // loop once for each character in <mUrl> after <ich>; set <ascii>
            // to the ASCII representation of runs of ASCII and non-ASCII characters
            // in <mUrl>; the idea is to reduce the number of times that
            // HttpUtility.UrlEncode() is called to reduce overhead
            StringBuilder ascii = new StringBuilder(this.mUrl.Length * 2);
            ascii.Append(this.mUrl.Substring(0, ich));
            StringBuilder nonAscii = new StringBuilder(this.mUrl.Length - ich);
            // contiguous run of non-ASCII characters
            while (true)
            {
                // set <ch> to the input character, or '\0' if we're at the end
                // of the URL
                char ch;
                if (ich < this.mUrl.Length)
                {
                    ch = this.mUrl[ich];
                }
                else
                {
                    ch = '\0';
                }

                // if <ch> is an input ASCII character or we reached the end of the
                // input string, and <nonAscii> contains previously-gathered input
                // Unicode characters, convert <nonAscii> to ASCII
                if ((ch <= '\x7F') && (nonAscii.Length > 0))
                {
                    ascii.Append(HttpUtility.UrlEncode(nonAscii.ToString()));
                    nonAscii.Length = 0;
                }

                // if we reached the end of the input string, we're done
                if (ch == '\0')
                {
                    break;
                }

                // process <ch>
                if (ch <= '\x7F')
                {
                    ascii.Append(ch); // <ch> is ASCII
                }
                else
                {
                    nonAscii.Append(ch); // <ch> is Unicode
                }

                // continue loop
                ich++;
            }

            // done
            return new UrlString(new Uri(ascii.ToString()));
        }

        /// <summary>
        /// Converts hexadecimal-encoded (%xx) non-ASCII UTF-8 sequences within a
        /// string to the Unicode equivalents.
        /// </summary>
        ///
        /// <returns>
        /// An <Typ>UrlString</Typ> with hexadecimal-encoded non-ASCII UTF-8
        /// sequences converted to Unicode equivalents.
        /// </returns>
        ///
        /// <remarks>
        /// This method looks for non-ASCII URL-encoded bytes within runs of
        /// hexadecimal-encoded (%xx) sequences in the string and converts those
        /// bytes to Unicode characters.  For example, "%c3%b1" is converted to
        /// "ñ", since byte 0xc3 followed by byte 0xb1 is the UTF-8 encoding for
        /// the character "ñ".  However, for example, "%20" is left unmodified.
        ///
        /// <p/>
        ///
        /// A characteristic of this method is that it converts an URL to an URL that's
        /// semantically equivalent: typing the string returned by the method into the
        /// address bar of Internet Explorer will yield the same result (i.e.
        /// navigate to the same location) as typing the input string.
        ///
        /// <p/>
        ///
        /// Unlike <Mth>/System.Web.HttpUtility.UrlDecode</Mth>, this method does
        /// not change "+" to " " (space).  Doing so might change the meaning of the URL,
        /// since "+" and " " aren't treated as the same character when they are
        /// part of the path component of the URL.  For example, although the
        /// following URLs point to the same location:
        ///
        /// <p>
        /// <pre>http://localhost/Foo.aspx?X=A B</pre>
        /// <pre>http://localhost/Foo.aspx?X=A+B</pre>
        /// </p>
        ///
        /// the following URLs point to different locations:
        ///
        /// <p>
        /// <pre>http://localhost/A B.aspx</pre>
        /// <pre>http://localhost/A+B.aspx</pre>
        /// </p>
        /// 
        /// Similarly, this method does not decode "%25" into "%".  If it did, then
        /// calling this method on the following URL:
        ///
        /// <p>
        /// <pre>http://localhost/LaNi%25c3%25b1a.htm</pre>
        /// </p>
        ///
        /// would yield the following result:
        ///
        /// <p>
        /// <pre>http://localhost/LaNi%c3%b1a.htm</pre>
        /// </p>
        ///
        /// which has a different meaning.  (The first URL refers to a document
        /// named "La%c3%25a.htm"&#8212;i.e. the file name itself contains "%"
        /// characters&#8212;while the second URL refers to a document named
        /// named "LaNiña.htm".)
        ///
        /// <p/>
        ///
        /// Characters that do not correspond to hexadecimal characters are not
        /// modified.  For example, "%JK" is not modified.
        ///
        /// </remarks>
        ///
        /// <example>
        ///
        /// The following code:
        ///
        /// <code language="C#">
        /// Console.WriteLine(new UrlString("http://localhost/LaNi%c3%b1a").ToUnicode());
        /// </code>
        ///
        /// writes the following output:
        ///
        /// <p>
        /// <pre>http://localhost/LaNiña</pre>
        /// </p>
        ///
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2234:PassSystemUriObjectsInsteadOfStrings")]
        public UrlString ToUnicode()
        {
            // Point <ich> to the first % character in <mUrl>; if none is
            // found, no conversion is required, so just return.
            int ich = 0;
            ich = this.mUrl.IndexOf('%');
            // no unicode characters, so no conversion needed
            if (ich == -1)
            {
                return this;
            }

            // Length of input string.
            int urlLength = this.mUrl.Length;

            // Algorithm: Search for runs of one or more "%xx" sequences (where xx is hexadecimal,
            // 80 to FF); unicodeBytes is the current run.  Example:
            // Input: mUrl = "http://localhost/LaNi%25%c3%b1a.htm"
            // State after "parsing" everything except "a.htm":
            // unicodeBytes = C3, B1
            // unicode = "http://localhost/LaNi%25ñ"
            // (then unicodeBytes is emptied)

            // The current list of unicode chars to translate.
            byte[] unicodeBytes = new byte[urlLength];
            int @byte = 0; // index into the uncodeBytes array for the next entry in the array

            // The output buffer.
            StringBuilder unicode = new StringBuilder(this.mUrl.Length);
            unicode.Append(this.mUrl.Substring(0, ich));

            // Keep track of whether we are currently in the middle of a run of unicode chars
            bool inUnicodeRun;

            while (ich < urlLength)
            {
                // while there are more chars within the input string...
                inUnicodeRun = (this.mUrl[ich] == '%'); // check if next char may start a new run
                // (don't know if this is a run yet -- it may be e.g. "%20")

                // If this isn't a unicode run, just append char to output buffer.
                if (!inUnicodeRun)
                {
                    // Copy all of the non-unicode chars until the next % to the output.
                    int nextUnicode = this.mUrl.IndexOf('%', ich);
                    if (nextUnicode == -1)
                    {
                        unicode.Append(this.mUrl, ich, urlLength - ich); // no more unicode chars
                        ich = urlLength;
                    }
                    else
                    {
                        unicode.Append(this.mUrl, ich, nextUnicode - ich);
                        ich = nextUnicode;
                    }
                }
                else
                {
                    // This is (maybe) a unicode run
                    while (inUnicodeRun)
                    {
                        // If the % sign is not at least 2 chars from the end, then this is the end of the input
                        // string, so we can skip some processing.
                        if (ich < urlLength - 2)
                        {
                            int unicodeByte1 = 0, unicodeByte2 = 0, unicodeByte3 = 0;

                            if (!this.GetEncodedValue(ich, ref unicodeByte1))
                            {
                                // It wasn't a valid number, so put the % into output string
                                this.EndUnicodeRun(unicodeBytes, ref @byte, unicode, false, ref inUnicodeRun, ref ich);
                            }
                            else if ((unicodeByte1 & 0x80) == 0)
                            {
                                // It does not require conversion, so add it to the output.
                                this.EndUnicodeRun(unicodeBytes, ref @byte, unicode, false, ref inUnicodeRun, ref ich);
                            }
                            else if ((unicodeByte1 & 0xE0) == 0xC0)
                            {
                                // In order for this to be valid, there must be another %HH value following
                                // this one.
                                if (this.GetEncodedValue(ich + 3, ref unicodeByte2))
                                {
                                    unicodeBytes[@byte++] = (byte)unicodeByte1;
                                    unicodeBytes[@byte++] = (byte)unicodeByte2;
                                    ich += 6; // skip the next 6 chars from input string (two %HH sequences)
                                }
                                else
                                {
                                    // It wasn't a valid unicode char, so put the string into output string
                                    this.EndUnicodeRun(
                                        unicodeBytes, ref @byte, unicode, true, ref inUnicodeRun, ref ich);
                                }
                            }
                            else if ((unicodeByte1 & 0xF0) == 0xE0)
                            {
                                // In order for this to be valid, there must be two more %HH values following 
                                // this one.
                                if (this.GetEncodedValue(ich + 3, ref unicodeByte2)
                                    && this.GetEncodedValue(ich + 6, ref unicodeByte3))
                                {
                                    unicodeBytes[@byte++] = (byte)unicodeByte1;
                                    unicodeBytes[@byte++] = (byte)unicodeByte2;
                                    unicodeBytes[@byte++] = (byte)unicodeByte3;

                                    ich += 9; // skip the next 6 chars from input string (three %HH sequences)
                                }
                                else
                                {
                                    this.EndUnicodeRun(
                                        unicodeBytes, ref @byte, unicode, false, ref inUnicodeRun, ref ich);
                                }
                            }
                            else
                            {
                                // It does not require conversion, so add it to the output.
                                this.EndUnicodeRun(unicodeBytes, ref @byte, unicode, false, ref inUnicodeRun, ref ich);
                            }
                        }
                        else
                        {
                            // no translation needed -- end of string of unicode encodings. So, add the rest of the 
                            // string and return.
                            this.EndUnicodeRun(unicodeBytes, ref @byte, unicode, true, ref inUnicodeRun, ref ich);
                        }

                        // If it so far seems that the next character could be part of the run, check it. Otherwise effectively
                        // this breaks out of the while loop.
                        inUnicodeRun = inUnicodeRun ? ((ich < urlLength) && (this.mUrl[ich] == '%')) : false;
                    }

                    // We just processed the final unicode char in the run, then append unicode characters to the output
                    AppendUnicodeChars(unicodeBytes, ref @byte, unicode);
                }
            }
            return new UrlString(unicode.ToString());
        }

        #region Private_ToUnicodeHelpers

        /// <summary>
        /// Process a character that indicates it is the end of a unicode run. The method writes the current character (or,
        /// optionally the remainder of the string) in mUrl and puts it into the output buffer after first writing the 
        /// characters in the unicode run.
        /// </summary>
        /// <param name="unicodeBytes">The list of bytes that were part of the run. They will be written to the output
        /// buffer. The list cannot be null, but may be empty.</param>
        /// <param name="byte">Number of bytes in the array. (This is not the size of the array, but rather the number 
        /// of bytes stored in the array.)</param>
        /// <param name="output">The output buffer to flush the unicode run and current character. It cannot be null.</param>
        /// <param name="flushInput">If true, the remainder of mUrl (starting from ich) will be written to the output
        /// buffer and ich is incremented accordingly.</param>
        /// <param name="inUnicodeRun">Bool indicating whether currently in the midst of a run of unicode 
        /// chars. On return, this will be set to false.</param>
        /// <param name="ich">The position of the current character in mUrl.</param>
        private void EndUnicodeRun(
            byte[] unicodeBytes,
            ref int @byte,
            StringBuilder output,
            bool flushInput,
            ref bool inUnicodeRun,
            ref int ich)
        {
            AppendUnicodeChars(unicodeBytes, ref @byte, output);
            if (flushInput)
            {
                // output remainder of input string
                output.Append(this.mUrl, ich, this.mUrl.Length - ich);
                ich = this.mUrl.Length;
            }
            else
            {
                // output only one char of input string
                output.Append(this.mUrl[ich]);
                ich++;
            }
            inUnicodeRun = false;
        }

        /// <summary>
        /// Takes a startLocation in mUrl that contains a %, return the integer that is the next two digits if those 
        /// digits are within the range for unicode characters (ie, valid hex).
        /// </summary>
        /// <param name="startLocation">This should indicate the location of a % character.</param>
        /// <param name="charValue">If the value following the % character (ie, at startLocation+1 and startLocation+2
        /// indicates an integer, it's returned here. The value is unchanged if the method returns false.</param>
        /// <returns>True if a valid integer is returned in <P>charValue</P>.</returns>
        private bool GetEncodedValue(int startLocation, ref int charValue)
        {
            string s1 = this.mUrl.Substring(startLocation + 1, 2);
            try
            {
                // Base 16 conversion to int. If the string is not hex, this will throw FormatException.
                charValue = Convert.ToInt16(s1, 16);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Given a collection of UTF-8 bytes, translate them to unicode characters and append them to the output
        /// buffer.
        /// </summary>
        /// <param name="unicodeBytes">Typed list of bytes that can be translated, assuming UTF-8 encoding,
        /// to unicode characters. May contain 0 elements, but cannot be null. The list of bytes must contain 
        /// precisely enough bytes to result in a list of characters. Extra bytes will be lost -- so don't send 
        /// any in!</param>
        /// <param name="byte">The index of the next byte to be added to the <P>unicodeBytes</P> array.</param>
        /// <param name="output">The output string to append the unicode characters to. Cannot be null.</param>
        private static void AppendUnicodeChars(byte[] unicodeBytes, ref int @byte, StringBuilder output)
        {
            // If the the unicodeBytes array is empty, do nothing.
            if (@byte == 0)
            {
                return;
            }

            output.Append(Encoding.UTF8.GetChars(unicodeBytes, 0, @byte));
            @byte = 0; // Since they have been written, remove them from pending byte array
            for (int i = 0; i < @byte; i++)
            {
                unicodeBytes[i] = 0;
            }
        }

        #endregion
    }

    /**

    <summary>
    [Internal class?]
    TODO
    </summary>

    <remarks>

    TODO...

    </remarks>

    <seealso cref="PlainTextString" />
    <seealso cref="HtmlString" />
    <seealso cref="JScriptString" />
    <seealso cref="UrlString" />

    */

    public class HtmlStringWriter : IDisposable
    {
        // see TODO#1 above
        /// <summary>
        /// The HtmlTextWriter wrapped by this class.  This HtmlTextWriter may
        /// write into a StringBuilder, a TextWriter, or a given HtmlTextWriter,
        /// depending on which /// HtmlStringWriter constructor to use.
        /// </summary>
        private HtmlTextWriter mHtmlTextWriter;

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            this.mHtmlTextWriter = new HtmlTextWriter(sw);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// 
        /// <param name="capacity">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter(int capacity)
        {
            StringBuilder sb = new StringBuilder(capacity);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            this.mHtmlTextWriter = new HtmlTextWriter(sw);
        }

        /// <summary>See <see cref="IDisposable.Dispose"/>.</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.mHtmlTextWriter != null)
                {
                    this.mHtmlTextWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="capacity">TODO</param>
        ///
        /// <param name="tab">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter(int capacity, string tab)
        {
            StringBuilder sb = new StringBuilder(capacity);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            this.mHtmlTextWriter = new HtmlTextWriter(sw, tab);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="writer">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter(TextWriter writer)
        {
            this.mHtmlTextWriter = new HtmlTextWriter(writer);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="writer">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter(HtmlTextWriter writer)
        {
            this.mHtmlTextWriter = writer;
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="writer">TODO</param>
        ///
        /// <param name="tab">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public HtmlStringWriter(TextWriter writer, string tab)
        {
            this.mHtmlTextWriter = new HtmlTextWriter(writer, tab);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="name">TODO</param>
        ///
        /// <param name="plaintext">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void AddAttribute(string name, PlainTextString plaintext)
        {
            this.mHtmlTextWriter.AddAttribute(name, plaintext);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="key">TODO</param>
        ///
        /// <param name="plaintext">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void AddAttribute(HtmlTextWriterAttribute key, PlainTextString plaintext)
        {
            this.mHtmlTextWriter.AddAttribute(key, plaintext);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="name">TODO</param>
        ///
        /// <param name="html">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void AddAttribute(string name, HtmlString html)
        {
            this.mHtmlTextWriter.AddAttribute(name, (string)html, false);
        }

        /// <summary>
        /// TODO 
        /// </summary>
        ///
        /// <param name="key">TODO</param>
        ///
        /// <param name="html">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void AddAttribute(HtmlTextWriterAttribute key, HtmlString html)
        {
            this.mHtmlTextWriter.AddAttribute(key, (string)html, false);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="name">TODO</param>
        ///
        /// <param name="jscript">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is being validated
        public void AddJavascriptProtocolAttribute(string name, JScriptString jscript)
        {
            FramesetUtil.ValidateNonNullParameter("jscript", jscript);
            this.mHtmlTextWriter.AddAttribute(name, jscript.ToJavascriptProtocol(), false);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="key">TODO</param>
        ///
        /// <param name="jscript">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is being validated
        public void AddJavascriptProtocolAttribute(HtmlTextWriterAttribute key, JScriptString jscript)
        {
            FramesetUtil.ValidateNonNullParameter("jscript", jscript);
            this.mHtmlTextWriter.AddAttribute(key, jscript.ToJavascriptProtocol(), false);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="tagName">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void RenderBeginTag(string tagName)
        {
            this.mHtmlTextWriter.RenderBeginTag(tagName);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="tagKey">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void RenderBeginTag(HtmlTextWriterTag tagKey)
        {
            this.mHtmlTextWriter.RenderBeginTag(tagKey);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void RenderEndTag()
        {
            this.mHtmlTextWriter.RenderEndTag();
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        public void EndRender()
        {
            this.mHtmlTextWriter.EndRender();
        }

        /// <summary>
        /// Indent written text by this number of tab positions.
        /// </summary>
        public int Indent
        {
            get
            {
                return this.mHtmlTextWriter.Indent;
            }
            set
            {
                this.mHtmlTextWriter.Indent = value;
            }
        }

        /// <summary>Writes a blank line.</summary>
        public void WriteLine()
        {
            this.mHtmlTextWriter.WriteLine();
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="plaintext">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void WriteText(PlainTextString plaintext)
        {
            this.mHtmlTextWriter.WriteEncodedText(plaintext);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="html">TODO</param>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        ///
        public void WriteHtml(HtmlString html)
        {
            this.mHtmlTextWriter.Write(html);
        }

        /// <summary>
        /// TODO
        /// </summary>
        ///
        /// <param name="writer">TODO</param>
        ///
        /// <returns>
        /// TODO
        /// </returns>
        ///
        /// <remarks>
        /// TODO
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // it is being validated
        public static implicit operator TextWriter(HtmlStringWriter writer)
        {
            FramesetUtil.ValidateNonNullParameter("writer", writer);
            return writer.mHtmlTextWriter;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}