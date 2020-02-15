from django.contrib import admin
from django.urls import path
from django.conf.urls import include
from . import views

urlpatterns = [
    path('wordMeaning/', views.wordMeaning, name='wordMeaning'),
    path('news/', views.news, name='news'),
    path('youtube/', views.youtube, name='youtube'),
    path('weatherReport', views.weatherReport, name='weatherReport'),
    path('OCR', views.OCR, name='OCR'),
]

