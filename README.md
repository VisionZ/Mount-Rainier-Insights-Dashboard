# Mount-Rainier-Insights-Dashboard

A user-responsive Kibana dashboard app that displays insightful climb and weather data  
that helps climbers plan for a trip to Mount Rainier.

Technologies Used: Elasticsearch, Kibana, .NET, C#

Inspiration:
https://www.kaggle.com/codersree/mount-rainier-weather-and-climbing-data

Getting Started:
1) Download Elasticsearch 7.2.1: https://www.elastic.co/downloads/past-releases/elasticsearch-7-2-1
2) Download Kibana 7.2.1: https://www.elastic.co/downloads/past-releases/kibana-7-2-1
3) Clone this repository
4) Open this repository with Visual Studio Code
5) In Visual Studio Code go to: Terminal -> New Terminal
6) In the VS Code terminal, navigate to the Mount-Rainier-Insights-Dashboard folder (the outermost directory) 
7) Install CsvTextFieldParser 1.2.1 and NEST 7.2.1 using dotnet add package
8) Run Elasticsearch.bat in the Elasticsearch bin folder
9) Run Kibana.bat in the Kibana bin folder
10) In the VS Code terminal, use dotnet run to index the data to Elasticsearch
11) In your browser go to http://localhost:5600 (this is the Kibana server)
12) Create a new dashboard and start playing around with the data!
