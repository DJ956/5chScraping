# -*- coding: utf-8 -*-
#1:CSVファイル群があるフォルダパス(/*)
#2:出力モデル(*.model)

import sys
import subprocess
import glob
import os
from gensim.models import word2vec

#CSVがあるフォルダからデータを取得してtxtに変換する
def generate(path):
  index = 0
  for file in glob.glob(path):       
    if file.find('.csv') > 0:
      name = "data_{}.txt".format(index)      
      cmd = "python mecab_csv.py {} {}".format(file, name)
      subprocess.call(cmd)      
      index = index + 1

#data_{}.txtファイルを1つのdata.txtにまとめる
def combine():
  fo = open("data.txt", 'a', encoding = "utf-8")

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
    generate(argv[1])

  combine()
  
  sentences = word2vec.LineSentence("data.txt")
  model = word2vec.Word2Vec(sentences,
                          sg=1,
                          size=100,
                          min_count=1,
                          window=10,
                          hs=1,
                          negative=0)
  model.save(argv[2])
  
if __name__ == '__main__':
	main()