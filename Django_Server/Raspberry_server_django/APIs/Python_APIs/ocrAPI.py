import tesserocr
from PIL import Image
import cv2

#apt-get install tesseract-ocr libtesseract-dev libleptonica-dev pkg-config
#pip install tesserocr
#CPPFLAGS=-I/usr/local/include pip install tesserocr
camera = cv2.VideoCapture(0)
return_value, image = camera.read()
image = Image.fromarray(image)
print(tesserocr.image_to_text(image))  # print ocr text from image
