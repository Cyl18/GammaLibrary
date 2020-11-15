# GammaLibrary

My own C# utilities library.  
It provides many useful extension methods and more.  
This project is licensed under [Anti-996 License](https://github.com/kattgu7/Anti-996-License).  
_May you be happy always._

## Examples

```CSharp
string a = null;
a.NotNullNorEmpty() // false
a.IsNullOrEmpty() // true

int[] b = { 1,2,3 };
b.PickOne() // randomly returns a element e.g. 3
b.NotContains(4) // true
string c = b.ToJsonString(); // [1, 2, 3]
int[] d = c.JsonDeserialize<int[]>() // [1, 2, 3] int array
string e = b.Connect(); // "1, 2, 3"
string f = b.Connect(separator: ".") // "1.2.3"
string f = b.Connect(separator: ".", prefix: "x") // "x1.2.3"

"abc".ToUTF8Bytes().SHA256().ToBase64String() // ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=
"abc".ToUTF8Bytes().ToUTF8String().ToUTF8Bytes().ToUTF8String()

"1".ToInt() // int 1
"1".IsInt() // true

"abc".SaveToFile("a.txt") // saves abc to file a.txt

Stream s = new MemoryStream();
s.CreateStreamWriter()
s.ToMemoryStream()
s.ReadToEnd()
```
