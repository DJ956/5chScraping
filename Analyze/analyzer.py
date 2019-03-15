# -*- coding: utf-8 -*-
#1:モデル(*.model)
#2:類義語(文字列)
#-*-coding:utf-8-*-
#1:data.txt
import MeCab
import re
import sys
import time

#名詞の中の一般と固有名詞だけをlistで返す
def filter(documents):
	tagger = MeCab.Tagger()
	wakati = tagger.parse(documents).split('\n')
	
	word_list = []
	for addlist in wakati:
		addlist = re.split('[\t,]', addlist)		
		if addlist == [] or addlist[0] == 'EOS' or addlist[0] == '' or addlist[0] == 'ー' or addlist[0] == '*':
			pass
		elif addlist[1] == '名詞':
			if addlist[2] == '一般' or addlist[2] == '固有名詞':
				word_list.append(addlist[0])

	return word_list

#テキストデータをロードして文字列を返す
def load(path):	
	doc = []
	with open(path, 'r', encoding = 'utf-8') as f:		
		line = f.readline()
		while line:
			doc.append(line)
			line = f.readline()
		print("総文字数:{}".format(len(doc)))

	return ''.join(doc)

def main():
	argv = sys.argv

	doc = load(argv[1])	
	word_list = filter(doc)
	
	print(word_list)

if __name__ == '__main__':
	main()