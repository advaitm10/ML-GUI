import pandas as pd
import sys

''' Takes filepath to the data as only argument '''

datapath = sys.argv[0]
pd.read_csv(datapath)

pString = ""
for i in pd.columns:
    pString += (i+",")
print(pString)