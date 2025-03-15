import json
import os

file = open(os.path.join(os.getcwd(), "Scripts", "JSONImport", "example.json"))
temp = json.loads(file.read())

for entry in temp:
    newFile = open(os.path.join(os.getcwd(), "JSONS", "Split", entry + ".json"),"x")
    newFile.write(json.dumps(temp[entry]))
    newFile.close()