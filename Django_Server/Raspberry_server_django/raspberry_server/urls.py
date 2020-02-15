"""raspberry_server URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/3.0/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from django.conf.urls import include
from . import views

urlpatterns = [
    path('admin/', admin.site.urls),
    path('call/', views.call, name='call'),
    path('listSongs/', views.listSongs, name='listSongs'),
    path('listVideos/', views.listVideos, name='listVideos'),
    path('listImages/', views.listImages, name='listImages'),
    path('setAlarm/', views.setAlarm, name='setAlarm'),
    path('captureImage/', views.captureImage, name='captureImage'),
    path('APIs/', include('APIs.urls'), name='APIs'),
]
