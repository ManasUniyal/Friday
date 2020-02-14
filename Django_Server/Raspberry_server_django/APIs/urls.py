from django.contrib import admin
from django.urls import path
from django.conf.urls import include
from . import views

urlpatterns = [
    path('wordMeaning', views.wordMeaning, name='wordMeaning'),
]

