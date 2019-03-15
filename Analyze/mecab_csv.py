#-*-coding:utf-8-*-
#1:入力CSVファイル名(*.csv)
#2:出力形態解析したテキストファイル名(*.txt)

import MeCab
import sys
import re

#邪魔な記号などを取り除く
def trim(source):
	t_half = re.compile(r'[!-~]')	#半角記号、数字、英字
	t_full = re.compile(r'[︰-＠]')	#全角記号
	t_full_2 = re.compile(r'[、・’〜：＜＞＿｜「」｛｝【】『』〈〉“”◯○〔〕…――――◇◆■●]')
	t_commma = re.compile(r'[。]')
	t_url = re.compile(r'https?://[\w/:%#\$&\?\(\)~\.=\+\-…]+')
	t_tag = re.compile(r"<[^>]*?>")	#HTML tag
	#t_n = re.compile(r'\n')	#改行文字
	t_space = re.compile(r'[\s+]')	#１以上の空白文字
	t_num = re.compile(r"[0-9]")

	source = t_half.sub("", source)
	source = t_full.sub("", source)
	source = t_full_2.sub("", source)
	source = t_commma.sub("", source)
	source = t_url.sub("", source)
	source = t_tag.sub("", source)
	#source = t_n.sub("", source)
	source = t_space.sub("", source)
	source = t_num.sub("", source)

	return source

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
			result = tagger.parse(trim(documents[2])) #形態解析、不必要な記号削除
			fo.write(result)
			fo.write("\n")

		line = fi.readline()

	fi.close()
	fo.close()


if __name__ == '__main__':
	main()