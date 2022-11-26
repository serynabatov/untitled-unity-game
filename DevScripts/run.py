import json
import download_files

memo = []


def go_through_subfolders(data, parent_folder):
    if "subfolders" not in data:
        download_files.download_folder(data["folder"], parent_folder + "/" + data["folder"])
        return
    
    for subfolders in data["subfolders"]:
        if data["folder"] not in memo:
            memo.append(data["folder"])
        go_through_subfolders(subfolders, parent_folder + "/" + data["folder"])
    download_files.download_folder(data["folder"], parent_folder)

            
if __name__ == "__main__":
    with open("folder_names.json") as file:
        data = json.load(file)

        # for folder_name in data:
        #     download_files.download_folder(folder_name)
        go_through_subfolders(data, ".")