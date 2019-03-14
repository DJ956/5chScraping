# -*- coding: utf-8 -*-
#1:モデル(*.model)
#2:類義語(文字列)

from gensim.models import word2vec
import sys

def main():
	argv = sys.argv

	model = word2vec.Word2Vec.load(argv[1])
	results = model.most_similar(positive=[argv[2]])
	for result in results:
		print(result)

if __name__ == '__main__':
	main()