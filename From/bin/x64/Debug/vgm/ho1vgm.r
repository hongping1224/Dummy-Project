rm(list = ls())
library(gstat)
library(sp)
args = commandArgs(trailingOnly=TRUE)
#path = "C:/Users/HongPing/Downloads/ProgramDescription/vgm"

tic <- Sys.time() 
{
  #filename = paste(path, "/ho1S4RPdtNZdsm.txt", sep = "")
  filename = args[1]
  station = read.table(filename, header = FALSE, col.names = c("x", "y", "z"))
  siterows = nrow(station)					   
  
  spsite = station
  coordinates(spsite) = ~x + y
  
  site.vgm = gstat::variogram(z ~ 1, spsite, width = 0.01, cutoff = 3.0)
  vgmrows = nrow(site.vgm)
  
  #filename = paste(path, "/ho1S4RPdtvgm1cm.txt", sep = "")
  filename = args[2]
  fp = file(filename, "w")
  cat("np dist gamma\n", file = fp, sep = "")
  for(index in 1:vgmrows)
  {
    cat(site.vgm$np[index], " ", 
        formatC(site.vgm$dist[index], digits = 15, width = -1, format = "fg"), " ",
        formatC(site.vgm$gamma[index], digits = 15, width = -1, format = "fg"), "\n", 
        file = fp, sep = "")         		
    
  }	
  close(fp) 
  
}
toc <- Sys.time()
comp.time <- toc - tic
lznr.fit = fit.variogram(site.vgm, model = vgm(0.01, "Sph", 2.0,  add.to = vgm(0.01, "Sph", 1, nugget = 0.000001)))
lznr.fit
capture.output(lznr.fit, file = "./variogram_modelling.txt")


