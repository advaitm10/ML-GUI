import sys
from fastai.tabular.all import *
import numpy as np
import pandas as pd

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

df = pd.read_csv(datapath)
dls = TabularDataLoaders.from_df(df, path=datapath, y_names=y,
    cat_names = categorical,
    cont_names = continuous,
    procs = [Categorify, FillMissing, Normalize])
learn = tabular_learner(dls, metrics=accuracy)
learn.fit_one_cycle(cycles)
learn.path = Path('.')
learn.export()