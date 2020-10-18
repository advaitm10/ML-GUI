public static string getOutput(string input) {           
  var inputString = input;
  var flag = "bruh";
  var bruhIndex = inputString.IndexOf(flag);
  if(bruhIndex < 0){
    return "error"
  }
  var output = inputString.Substring(bruhIndex + flag.Length);
  return output;
}
