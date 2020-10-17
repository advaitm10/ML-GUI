import pandas as pd
import sys

''' Takes filepath to the data as only argument '''

datapath = sys.argv[1]
df = pd.read_csv(datapath)

pString = ""
for i in df.columns:
    pString += (i+",")
print(pString)