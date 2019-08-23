import cv2


filename = "S4_zerocontour.bmp"
img = cv2.imread(filename)
crop_img = img[87:2699,87:2699]

cv2.imwrite("crop.bmp",crop_img)
