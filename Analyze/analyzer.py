# -*- coding: utf-8 -*-
#1:data.txt
#2:出力結果(*.txt)
#3:最頻出単語表示数

import MeCab
import re
import sys
import matplotlib.pyplot as plt

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

#単語の出現回数をカウント
def count(word_list, show_count):
	dicts = {}
	for count in word_list:
		if count not in dicts:
			dicts.setdefault(count, 1)
		else:
			dicts[count] += 1

	total = sum(dicts.values())	
	print("総単語数:{}".format(total))

	sorted_dict = {}
	index = 0
	for k, v in sorted(dicts.items(), key=lambda x: x[1], reverse=True):
		sorted_dict.update({str(k):int(v)})
		index += 1
		if index > show_count:
			break

	return sorted_dict, total

#結果をグラフにして出力する
def create_plt(dicts, show_count, total):
	plt.figure(figsize=(15, 5))
	plt.title(u'最頻出ワードベスト:{0} 総単語数:{1}'.format(show_count, total), size=16)
	plt.bar(range(len(dicts)), list(dicts.values()), align='center')
	plt.xticks(range(len(dicts)), list(dicts.keys()))

	for x, y in zip(range(len(dicts)), dicts.values()):
		plt.text(x, y, y, ha='center', va='bottom')
		plt.text(x, y/2, u'{0}%'.format(round((y/total*100), 3)), ha='center', va='bottom')
	plt.tick_params(width=2, length=10)
	plt.tight_layout()

	plt.savefig("result.png")

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

def save(path, dicts, show_count):
	index = 0
	with open(path, 'w', encoding='utf-8') as fo:
		for k, v in dicts.items():
			fo.write("{0}:{1}\n".format(k, v))			

def main():
	argv = sys.argv

	load_path = argv[1]
	out_path  = argv[2]
	show_count = int(argv[3])

	print("テキストデータロード開始...")
	doc = load(load_path)	
	print("テキストデータロード完了")

	print("固有名詞のみを抽出中...")
	word_list = filter(doc)
	print("抽出完了")

	print("単語の出現回数をカウント中...")
	dicts, total = count(word_list, show_count)

	print("結果を出力中...")
	create_plt(dicts, show_count, total)
	save(out_path, dicts, show_count)
	print("出力完了:{}".format(out_path))
	

if __name__ == '__main__':
	main()