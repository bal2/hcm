export class GroupModel {
    groupId: number;
    name: string;
    shortName: string;
    description: string;
    createdAt: Date;
    updatedAt: Date;
}

export class NewGroupModel {
    name: string;
    shortName: string;
    description: string;
}