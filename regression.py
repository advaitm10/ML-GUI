import sys
from fastai.tabular.all import *
import numpy as np
import pandas as pd

'''
     arguments should be a file path for the data, y name, categorical names, continuous names

     Notes: Figure out how to allow user to use their own model
'''

#take in data file paths using system args
args = sys.argv
datapath = args[1]
y = args[2]
categorical = args[3].split(",")
continuous = args[4].split(",")


datapath = untar_data(URLs.ADULT_SAMPLE)
df = pd.read_csv(datapath/'adult.csv')
dls = TabularDataLoaders.from_df(df, path=datapath, y_names=y,
    cat_names = categorical,
    cont_names = continuous,
    procs = [Categorify, FillMissing, Normalize])
learn = tabular_learner(dls, metrics=accuracy)
learn.fit_one_cycle(1)
learn.path = Path('.')
learn.export()