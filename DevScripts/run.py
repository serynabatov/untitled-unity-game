import json
import download_files
import argparse

memo = []


def go_through_subfolders(data, parent_folder, upload=False):
    if "subfolders" not in data:
        if upload == "False":
            download_files.download_folder(
                data["folder"], parent_folder + "/" + data["folder"])
        else:
            download_files.upload_files(data["folder"], parent_folder + "/" + data["folder"])
        return

    for subfolders in data["subfolders"]:
        if data["folder"] not in memo:
            memo.append(data["folder"])
        value = data["folder"]
        if value == "":
            go_through_subfolders(subfolders, parent_folder, upload)
        else:
            go_through_subfolders(subfolders, parent_folder + "/" + value, upload)
    if upload == "False":
        download_files.download_folder(data["folder"], parent_folder + "/" + data["folder"])
    else:
        download_files.upload_files(data["folder"], parent_folder + "/" + data["folder"])

if __name__ == "__main__":
    parser = argparse.ArgumentParser(
        prog='Google API script',
        description='Downloads and uploads the files to the Google Drive'
    )
    parser.add_argument('-u', '--upload')

    args = parser.parse_args()

    with open("folder_names.json") as file:
        data = json.load(file)

        go_through_subfolders(data, "../Assets/Sprites", args.upload)
