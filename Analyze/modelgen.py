# -*- coding: utf-8 -*-
#1:CSVファイル群があるフォルダパス(/*)
#2:出力モデル(*.model)

import sys
import subprocess
import glob
import os
from gensim.models import word2vec

out = "data.txt"

#CSVがあるフォルダからデータを取得してtxtに変換する
def generate(path):
  index = 0
  files = glob.glob(path + "/*.csv")
  size = len(files)

  for file in files:           
    print("\r{}/{}-{}を解析...".format(size, index, file), end="")

    name = "data_{}.txt".format(index)      
    cmd = "python mecab_csv.py {} {}".format(file, name)
    subprocess.call(cmd)      
    index = index + 1

#data_{}.txtファイルを1つのdata.txtにまとめる
def combine():
  fo = open(out, 'a', encoding = "utf-8")

  for file in glob.glob("./data_*.txt"):
    fi = open(file, 'r', encoding = "utf-8")
    line = fi.readline()
    while line:
      fo.write(line)        
      line = fi.readline()
    fi.close()

  fo.close()
  #大量に生成されたファイルの後始末
  for file in glob.glob("./data_*.txt"):
    os.remove(file)

def main():
  argv = sys.argv
  #CSVフォルダ結合をしない場合は1文字適当に打ち込む
  if len(argv[1]) > 5:
    print("CSVファイルの形態解析開始")
    generate(argv[1])
    print("CSVフアィルの形態解析完了")

  

  print("テキストデータ結合開始")
  combine()
  print("テキストデータ結合完了 - {}".format(out))
  
  print("データモデル生成開始")
  sentences = word2vec.LineSentence(out)
  model = word2vec.Word2Vec(sentences,
                          sg=1,
                          size=100,
                          min_count=1,
                          window=10,
                          hs=1,
                          negative=0)
  model.save(argv[2])
  print("データモデル生成完了 - {}".format(argv[2]))
  
if __name__ == '__main__':
	main()