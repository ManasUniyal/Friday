from PIL import Image
import cv2

def detect_document(path):
	global resultString
	"""Detects document features in an image."""
	from google.cloud import vision
	import io
	client = vision.ImageAnnotatorClient()
	with io.open(path, 'rb') as image_file:
		content = image_file.read()
	image = vision.types.Image(content=content)
	response = client.document_text_detection(image=image)
	for page in response.full_text_annotation.pages:
		for block in page.blocks:
			print('\nBlock confidence: {}\n'.format(block.confidence))
			
			for paragraph in block.paragraphs:
				print('Paragraph confidence: {}'.format(
					paragraph.confidence))
				for word in paragraph.words:
					word_text = ''.join([
						symbol.text for symbol in word.symbols
					])
					resultString += word_text+"#"
					print(word_text)
					print('Word text: {} (confidence: {})'.format(
						word_text, word.confidence))
	if response.error.message:
		raise Exception(
			'{}\nFor more info on error messages, check: '
			'https://cloud.google.com/apis/design/errors'.format(
				response.error.message))

	import os
	os.remove("live.jpeg")
	print("File Removed!")

resultString = str()
camera = cv2.VideoCapture(0)
return_value, image = camera.read()
file = 'live.jpeg'
cv2.imwrite(file, image)
detect_document("live.jpeg")
print('ans = '+resultString)
