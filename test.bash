cd /home/eug0/Projects/TodoApp

git filter-repo --force \
  --path SUBDIR/ \
  --partial \
  --email-callback '
if email == b"mahdi@panto.org":
    return b"mahdi.jz.v@gmail.com"
return email
' \
  --name-callback '
if name == b"mahdi-javidi":
    return b"mahdijz5"
return name
'
