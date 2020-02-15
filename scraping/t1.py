import os
#ques = "Give the dimensions of: (a) the volume of a cube of edge x, (b) the volume of a sphere of radius x, (c) the ratio of the volume of a cube of edge x to the the volume of a sphere of radius x?"
ques = "A charge of 8 mC is located at the origin. Calculate the work done intaking a small charge of –2 × 10–9 C from a point P (0, 0, 3 cm) to a point Q (0, 4 cm, 0), via a point R (0, 6 cm, 9 cm)."
ques1 = ques.replace(" ", "+").replace("\\n", "+").replace("\\t", "+").replace("\n", "+").replace("(", "+").replace(")", "+")
os.system(f'scrapy crawl spider -a question={ques1}  -a _id=0')