import numpy as np
import pandas as pd
import sys
import torch
import os
from fastai.text.all import *



def main():

	torch.cuda.empty_cache()


	#take in data file paths using system args
	args = sys.argv
	datapath = args[1]
	text_column = args[2]
	label_column = args[3]
	test_case = args[4]

	#Read CSV -> Dataframe -> Langauge Modeling and Language Classification Data
	df = pd.read_csv (datapath)
	dls_lm = TextDataLoaders.from_df(df, text_col=text_column, label_col =label_column , is_lm=True, bs=32)
	dls_clas = TextDataLoaders.from_df(df, text_col='review', label_col='sentiment', bs=32)

	#Build Language Modeling Model
	learn = language_model_learner(dls_lm, AWD_LSTM, drop_mult=0.5)
	learn.fit_one_cycle(1, 1e-2)
	learn.unfreeze()
	learn.fit_one_cycle(3, slice(1e-4,1e-2))

	#Output Language Modeling Result
	#print(learn.predict("This is a review about", n_words=10))

	#Build Language Classification
	learn.save('mini_imdb_language_model')
	learn.save_encoder('mini_imdb_language_model_encoder')
	learn = text_classifier_learner(dls_clas, AWD_LSTM, drop_mult=0.5)
	learn.load_encoder('mini_imdb_language_model_encoder')
	
	#Output Language Classification Result
	print(learn.predict(test_case))

if __name__ == '__main__':
    # freeze_support() here if program needs to be frozen
    main()  # execute this only when run directly, not when imported!

#learn = language_model_learner(data_lm, AWD_LSTM, drop_mult=0.5)
#learn.fit_one_cycle(1, 1e-2)
#learn.unfreeze()
#learn.fit_one_cycle(3, slice(1e-4,1e-2))
#learn.predict("This is a review about", n_words=10)


#learn = text_classifier_learner(dls_clas, AWD_LSTM, metrics=accuracy)
#learn.fit_one_cycle(1, 2e-2, moms=(0.8,0.7,0.8))