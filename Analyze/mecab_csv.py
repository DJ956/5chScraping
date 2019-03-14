#-*-coding:utf-8-*-
#1:入力CSVファイル名(*.csv)
#2:出力形態解析したテキストファイル名(*.txt)

import MeCab
import sys

def main():
	argv = sys.argv
	tagger = MeCab.Tagger('-O wakati')

	fi = open(argv[1], 'r', encoding="utf-8")
	fo = open(argv[2], 'w', encoding="utf-8")

	line = fi.readline()

	skip = True
	while line:
		if skip == True:
			skip = False
			continue

		documents = line.split(',')
		if len(documents) != 4:
			line = fi.readline()
			continue
		else:
			result = tagger.parse(documents[2])
			fo.write(result[1:])

		line = fi.readline()

	fi.close()
	fo.close()


if __name__ == '__main__':
	main()