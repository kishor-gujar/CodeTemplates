// add this inot program.cs file

var cultureInfo = new CultureInfo("en-IN"); // Set to Indian culture
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


  <td>@item.SpecialDateAmount.ToString("C")</td>
  <td>@item.GST.ToString("C")</td>
  <td>@item.Amount.ToString("C")</td>