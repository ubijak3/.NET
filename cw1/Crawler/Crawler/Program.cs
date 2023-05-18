using System.Text.RegularExpressions;

string url;
if (args.Length == 0) { throw new ArgumentNullException(); }
else
{
    url = args[0];
}
Uri uriResult;
bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
if (!result)
{
    throw new ArgumentException("Niepoprawny url");
}
var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
var set = new HashSet<string>();

var httpClient = new HttpClient();
var httpResult = await httpClient.GetAsync(url);
if (!httpResult.IsSuccessStatusCode) throw new Exception("Błąd w czasie pobierania strony");
var httpContent = await httpResult.Content.ReadAsStringAsync();
var matches = regex.Matches(httpContent);
foreach (Match match in matches)
{
    set.Add(match.Value);
}
if (set.Count() == 0) throw new Exception("Nie znaleziono żadnych adresow email");

matches.Select(e => e.Value).Distinct().ToList().ForEach(e => Console.WriteLine(e));