import { FileInfoModel } from './file-info.model';

export class FileInfoGroupModel {

    constructor() {
        this.files = [];
    }

    public extension: string;
    public files: FileInfoModel[];
}