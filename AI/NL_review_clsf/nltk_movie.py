import os
import string
import pandas as pd
import numpy as np
from nltk.corpus import stopwords
from nltk.probability import FreqDist
from nltk.tokenize import word_tokenize
from nltk import NaiveBayesClassifier
from nltk.classify import accuracy
from itertools import chain

train_path = './data/train'
test_path = './data/test'

stop_word = stopwords.words('english')
punc_word = string.punctuation


def train_labels():
    labels_train = pd.read_csv('./data/labels_train.csv')
    labels_arr = np.array(labels_train[['ReviewID', 'Rating']])
    return {r_id: rating for r_id, rating in labels_arr}


def reviews_doc(path, labels = {}):
    reviews = []
    
    for file in os.listdir(path)[:200]:
        review = open(path + "/" + file, 'r').read()
        
        review = [w for w in word_tokenize(review)
                  if w.lower() not in stop_word and
                  w.lower() not in punc_word]
        
        label = labels[file.split('.')[0]] or ''
        reviews.append((review, label))
        
    return reviews



def doc_feature(all_feature, doc):
    doc_words = set(doc)
    features = {}
    
    for word in all_feature:
        features[word] = (word in doc_words)
        
    return features


def make_features_sets(reviews_doc, all_feature):
    features_sets = [(doc_feature(all_feature, doc), lbl) for doc,lbl in reviews_doc]
    return features_sets


def main():
    
    _labels = train_labels()
    train_reviews_doc = reviews_doc(train_path, _labels)[:100]
    all_feature = FreqDist(chain(*[review for review, _ in train_reviews_doc]))
    train_features = make_features_sets(train_reviews_doc, all_feature)
    clsf = NaiveBayesClassifier.train(train_features)
    test_reviews_doc = reviews_doc(train_path, _labels)[100:]
    test_features = make_features_sets(test_reviews_doc, all_feature)
    test_lbl = clsf.classify_many([f for f,_ in test_features])
    
    print("\ntest_result:\n", test_lbl)
    print("\n\n", accuracy(clsf, test_features))
    clsf.show_most_informative_features(10)


main()

