#-*-coding:utf-8-*-
#1:入力txtファイル名(*.txt)
#2:出力形態解析したテキストファイル名(*.txt)

import MeCab
import sys

def main():
	argv = sys.argv
	tagger = MeCab.Tagger('-O wakati')

	fi = open(argv[1], 'r', encoding="utf-8")
	fo = open(argv[2], 'w', encoding="utf-8")

	line = fi.readline()

	while line:
		result = tagger.parse(line)
		fo.write(result[1:])
		line = fi.readline()

	fi.close()
	fo.close()


if __name__ == '__main__':
	main()