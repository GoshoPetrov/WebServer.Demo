using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Server.View
{
    public class Form
    {
        public const string Html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>Simple Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .container {
            background: white;
            padding: 20px 30px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            width: 300px;
        }
        h2 {
            text-align: center;
        }
        label {
            display: block;
            margin-top: 10px;
        }
        input {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            box-sizing: border-box;
        }
        button {
            margin-top: 15px;
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        button:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>

<div class=""container"">
    <h2>User Form</h2>
    {0}
    <form method=""POST"" action=""/form"">
        
        <label for=""name"">Name:</label>
        <input type=""text"" id=""name"" name=""name"" placeholder=""Enter your name"" required>

        <label for=""age"">Age:</label>
        <input type=""number"" id=""age"" name=""age"" placeholder=""Enter your age"" required>

        <button type=""submit"">Save</button>

    </form>
</div>

</body>
</html>

";
    }
}
