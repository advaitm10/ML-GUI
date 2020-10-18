import sys
from fastai.tabular.all import *
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import torch

'''
     arguments: file path for the data, y name, categorical names, continuous names
     returns: for each cycle: row of strings with epoch, train_loss, valid_loss, accuracy, time. Split by blank space

     Notes: Figure out how to allow user to use their own model
'''

# pd.categorify
# DataFrame.dropna

#take in data file paths using system args
args = sys.argv
datapath = args[1]
y = args[2]
categorical = args[3].split(",")
continuous = args[4].split(",")
cycles = int(args[5])
plot = int(args[6])
if(len(args) > 7):
    point = args[7]
else:
    point = None

df = pd.read_csv(datapath)
dls = TabularDataLoaders.from_df(df, path=datapath, y_names=y,
    cat_names = categorical,
    cont_names = continuous,
    procs = [Categorify, FillMissing, Normalize])
learn = tabular_learner(dls, metrics=accuracy)
learn.fit_one_cycle(20)

learn.path = Path('.')
learn.export("export.pkl")

if(plot == "1"):
    preds, targs = learn.get_preds(with_loss=False)

    preds = preds.numpy()
    targs = targs.numpy()

    plt.ylabel("Predictions")
    plt.xlabel("Actual Values")
    print(preds)
    print(targs)
    plt.plot([0, max(np.max(preds[:, 1]), np.max(targs))], [0, max(np.max(preds[:, 1]), np.max(targs))], label = "Perfect Line")
    plt.plot(targs, preds, "ro", label = "Actual vs Predicted")
    plt.show()

if(point != None):
    df = pd.read_csv("point.csv")
    row, clas, probs = learn.predict(df.iloc[0])
    print(row.get_value(0, 'salary'))